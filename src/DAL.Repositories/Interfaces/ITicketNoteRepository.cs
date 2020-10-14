namespace DAL.Repositories.Interfaces
{
    using Models.Domain.Models;
    using Models.Filters;
    using System.Collections.Generic;

    public interface ITicketNoteRepository : IRepository<TicketNote>
    {
        List<TicketNote> Search(TicketNoteFilter filter);
        long Count(TicketNoteFilter filter);
    }
}
