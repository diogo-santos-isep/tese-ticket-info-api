namespace BLL.Services.Implementations
{
    using BLL.Services.Interfaces;
    using DAL.Repositories.Interfaces;
    using Models.Domain.Models;
    using Models.DTO.Grids;
    using Models.Filters;
    using System;

    public class TicketNoteService : ITicketNoteService
    {
        private ITicketNoteRepository _repo;

        public TicketNoteService(ITicketNoteRepository repo)
        {
            this._repo = repo;
        }
        public TicketNote Create(TicketNote model)
        {
            return this._repo.Create(model);
        }

        public TicketNoteGrid Search(TicketNoteFilter filter)
        {
            return new TicketNoteGrid
            {
                List = this._repo.Search(filter),
                Count = Convert.ToInt32(this._repo.Count(filter))
            };
        }

    }
}
