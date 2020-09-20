namespace BLL.Services.Extensions
{
    using Models.Domain.Models;
    using System.Collections.Generic;
    using System.Linq;

    public static class TicketExtensions
    {
        public static decimal GetScore(this IEnumerable<Ticket> tickets)
        {
            return tickets.Where(t=>t.Priority>0).Sum(t => 1/t.Priority);
        }
    }
}
