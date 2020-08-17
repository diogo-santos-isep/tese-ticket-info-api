using BLL.Services.Interfaces;
using DAL.Repositories.Interfaces;
using Models.Domain.Models;
using System;
using System.Collections.Generic;

namespace BLL.Services.Implementations
{
    public class TicketService : ITicketService
    {
        private ITicketRepository _repo;

        public TicketService(ITicketRepository _repo)
        {
            this._repo = _repo;
        }
        public Ticket Create(Ticket model)
        {
            return _repo.Create(model);
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
