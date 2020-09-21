namespace DAL.Repositories.Extensions
{
    using Models.Domain.Models;
    using Models.Filters;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class TicketFilterExtensions
    {
        public static FilterDefinition<Ticket> BuildFilters(this TicketFilter filter)
        {
            var arr = new List<FilterDefinition<Ticket>>();
            if (!String.IsNullOrEmpty(filter.ClientId))
                arr.Add(Builders<Ticket>.Filter.Eq(x => x.ClientId, filter.ClientId));
            if (!String.IsNullOrEmpty(filter.CollaboratorId))
                arr.Add(Builders<Ticket>.Filter.Eq(x => x.CollaboratorId, filter.CollaboratorId));
            if(filter.States != null && filter.States.Count()>0)
                arr.Add(Builders<Ticket>.Filter.In(x => x.State, filter.States));
            if(filter.Priority.HasValue && filter.Priority.Value> 0)
                arr.Add(Builders<Ticket>.Filter.Eq(x => x.Priority, filter.Priority));
            if (arr.Count == 0)
                arr.Add(Builders<Ticket>.Filter.Empty);
            return Builders<Ticket>.Filter.And(arr);
        }

        public static SortDefinition<T> BuildSort<T>(this Filter filter)
        {
            return filter.SortAscending ? Builders<T>.Sort.Ascending(filter.SortBy) : Builders<T>.Sort.Descending(filter.SortBy);
        }
    }
}
