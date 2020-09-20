using System;

namespace Models.Filters
{
    public class TicketFilter : Filter
    {
        public string ClientId { get; set; }
        public string CollaboratorId { get; set; }

        public TicketFilter()
        {
            this.SortBy = "Date";
        }
    }
}
