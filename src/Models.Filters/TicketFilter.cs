namespace Models.Filters
{
    using Models.Domain.Enums;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class TicketFilter : Filter
    {
        public string ClientId { get; set; }
        public string CollaboratorId { get; set; }
        public IEnumerable<ETicketState> States { get; set; }
        public int? Priority { get; set; }

        public TicketFilter()
        {
            this.SortBy = "Date";
        }
    }
}
