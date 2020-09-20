namespace DAL.Repositories.Interfaces
{
    using Models.Domain.Models;
    using Models.Filters;
    using System.Collections.Generic;

    public interface ITicketRepository : IRepository<Ticket>
    {
        List<Ticket> Search(TicketFilter filter);
        long Count(TicketFilter filter);
    }
}
