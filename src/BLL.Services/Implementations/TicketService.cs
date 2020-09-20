namespace BLL.Services.Implementations
{
    using BLL.RabbitMQ.Producers.Bodies;
    using BLL.RabbitMQ.Producers.Interfaces;
    using BLL.Services.Interfaces;
    using DAL.Repositories.Interfaces;
    using Models.Domain.Enums;
    using Models.Domain.Models;
    using Models.DTO.Grids;
    using Models.Filters;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TicketService : ITicketService
    {
        private ITicketRepository _repo;
        private ITicketReassignService _reassignService;
        private ITicketStateChangedEventProducer _stateChangedProducer;
        private ITicketCreatedEventProducer _ticketCreatedEventProducer;

        public TicketService(ITicketRepository _repo, ITicketStateChangedEventProducer stateChangedProducer,
            ITicketReassignService reassignService, ITicketCreatedEventProducer ticketCreatedEventProducer)
        {
            this._repo = _repo;
            this._reassignService = reassignService;
            this._stateChangedProducer = stateChangedProducer;
            this._ticketCreatedEventProducer = ticketCreatedEventProducer;
        }

        public Ticket Create(string message)
        {
            var model = Ticket.GenerateFromMessage(message);
            model = Create(model);

            //TODO Send message

            return model;
        }
        public Ticket Create(Ticket model)
        {
            model = ApplyCreateProperties(model);

            model = _repo.Create(model);
            _ = this._ticketCreatedEventProducer.Produce(TicketCreatedEventBody.BuildMessage(model));
            _ = this._reassignService.AssignTicket(model);

            return model;
        }

        private Ticket ApplyCreateProperties(Ticket model)
        {
            model.Date = DateTime.Now;
            model.Code = GetNextCode();
            model.State = ETicketState.Created;

            return model;
        }

        private string GetNextCode()
        {
            var filter = new TicketFilter
            {
                Page = 1,
                PageSize = 1,
                SortAscending = false,
                SortBy = "Date"
            };
            var search = Search(filter);
            var prevNumber = search.Count > 0 ? search.List.First().Code.Split('-')[1] : "0";
            return "T-" + (Int32.Parse(prevNumber)+1);
        }

        public void Delete(string id) => _repo.Delete(id);

        public List<Ticket> Get() => _repo.Get();

        public Ticket Get(string id) => _repo.Get(id);

        public TicketClientVMGrid SearchForClient(TicketFilter filter)
            => new TicketClientVMGrid(Search(filter));

        public TicketGrid Search(TicketFilter filter)
        {
            return new TicketGrid
            {
                List = _repo.Search(filter),
                Count = Convert.ToInt32(_repo.Count(filter))
            };
        }

        public Ticket Update(string id, Ticket model)
        {
            var previous = Get(model.Id);
            _repo.Update(id, model);

            if (previous == null || previous.StateIsDifferent(model))
                _ = this._stateChangedProducer.Produce(TicketStateChangedEventBody.BuildMessage(model));

            return model;
        }
    }
}
