namespace BLL.Services.Implementations
{
    using BLL.RabbitMQ.Producers.Bodies;
    using BLL.RabbitMQ.Producers.Interfaces;
    using BLL.Services.Extensions;
    using BLL.Services.Interfaces;
    using DAL.Clients.Interfaces;
    using DAL.Repositories.Interfaces;
    using Models.Domain.Enums;
    using Models.Domain.Models;
    using Models.Filters;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class TicketReassignService : ITicketReassignService
    {
        private ITicketRepository _ticketRepository;
        private ITicketReassignedEventProducer _ticketReassignedEventProducer;
        private ITicketStateChangedEventProducer _stateChangedProducer;
        private IDepartmentClient _departmentClient;
        private IUserClient _userClient;

        public TicketReassignService(IDepartmentClient departmentClient, IUserClient userClient
            , ITicketRepository ticketRepository, ITicketReassignedEventProducer ticketReassignedEventProducer
            , ITicketStateChangedEventProducer stateChangedProducer)
        {
            this._departmentClient = departmentClient;
            this._userClient = userClient;
            this._ticketRepository = ticketRepository;
            this._ticketReassignedEventProducer = ticketReassignedEventProducer;
            this._stateChangedProducer = stateChangedProducer;
        }
        public async Task AssignTicket(Ticket model)
        {
            try
            {
                var isAssigned = model.State == ETicketState.Assigned;
                var defaultDepartment = await this._departmentClient.GetDefaultDepartment().ConfigureAwait(false);
                var collaborators = await this._userClient.GetCollaboratorsFromDepartment(defaultDepartment).ConfigureAwait(false);

                var collaborator = GetMostFreeCollaborator(collaborators);
                if (collaborator == null)
                    return;
                model = model.AssignCollaborator(collaborator);
                this._ticketRepository.Update(model.Id, model);
                _ = this._ticketReassignedEventProducer.Produce(TicketReassignedEventBody.BuildMessage(model));
                if (!isAssigned)
                    _ = this._stateChangedProducer.Produce(TicketStateChangedEventBody.BuildMessage(model));
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro ao atribuir o ticket: {ex.Message}");
                throw;
            }
        }

        public User GetMostFreeCollaborator(IEnumerable<User> collaborators)
        {
            var tuples = collaborators.Select(c => {
                var tickets = this._ticketRepository.Search(new TicketFilter { CollaboratorId = c.Id });
                var score = tickets.GetScore();
                return new Tuple<User, decimal>(c, score);
            }).OrderBy(t=>t.Item2);

            return tuples.First()?.Item1;
        }
    }
}
