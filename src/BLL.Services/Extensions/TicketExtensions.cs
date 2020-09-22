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
        public static IEnumerable<string> GetChanges(this Ticket ticket, Ticket otherTicket)
        {
            var list = new List<string>();
            if (ticket.Description != otherTicket.Description)
                list.Add("Descrição");
            if (ticket.Subject != otherTicket.Subject)
                list.Add("Assunto");
            if (ticket.Priority != otherTicket.Priority)
                list.Add("Prioridade");

            return list;
        }
    }
}
