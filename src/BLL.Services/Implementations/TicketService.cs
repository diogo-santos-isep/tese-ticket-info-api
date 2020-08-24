using BLL.Services.Interfaces;
using DAL.RabbitMQ.Producers.Bodies;
using DAL.RabbitMQ.Producers.Interfaces;
using DAL.Repositories.Interfaces;
using Models.Domain.Models;
using System;
using System.Collections.Generic;

namespace BLL.Services.Implementations
{
    public class TicketService : ITicketService
    {
        private ITicketRepository _repo;
        private ITicketStateChangedProducer _stateChangedProducer;

        public TicketService(ITicketRepository _repo, ITicketStateChangedProducer stateChangedProducer)
        {
            this._repo = _repo;
            this._stateChangedProducer = stateChangedProducer;
        }
        public Ticket Create(Ticket model)
        {
            var previous = String.IsNullOrEmpty(model.Id) ? null : Get(model.Id);
            var ticket = _repo.Create(model);

            if (previous == null || previous.StateIsDifferent(model)) 
                _ = this._stateChangedProducer.Produce(TicketStateChangedBody.BuildMessage(model));

            return ticket;
        }

        public void Delete(string id) => _repo.Delete(id);

        public List<Ticket> Get() => _repo.Get();

        public Ticket Get(string id) => _repo.Get(id);

        public Ticket Update(string id, Ticket model) {
            _repo.Update(id, model);
            return model;
        }
    }
}
