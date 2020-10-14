namespace DAL.Repositories.Implementations
{
    using DAL.Repositories.Extensions;
    using DAL.Repositories.Interfaces;
    using Models.Domain.Models;
    using Models.Filters;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;

    public class TicketNoteRepository : ITicketNoteRepository
    {
        private readonly string COLLECTIONNAME = "TicketNotes";
        private readonly IMongoCollection<TicketNote> _collection;

        public TicketNoteRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<TicketNote>(COLLECTIONNAME);
        }
        public TicketNote Create(TicketNote model)
        {
            _collection.InsertOne(model);
            return model;
        }

        public void Delete(string id) =>
            _collection.DeleteOne(book => book.Id == id);

        public List<TicketNote> Get() =>
            _collection.Find(book => true).ToList();

        public TicketNote Get(string id) =>
            _collection.Find<TicketNote>(book => book.Id == id).FirstOrDefault();

        public void Update(string id, TicketNote model) =>
            _collection.ReplaceOne(book => book.Id == id, model);
        public List<TicketNote> Search(TicketNoteFilter filter)
        {
            var filters = filter.BuildFilters();
            var sort = filter.BuildSort<TicketNote>();
            return _collection
                    .Find(filters)
                    .Sort(sort)
                    .Skip((filter.Page - 1) * filter.PageSize)
                    .Limit(filter.PageSize)
                    .ToList();
        }

        public long Count(TicketNoteFilter filter)
            =>
                _collection
                    .CountDocuments(filter.BuildFilters());
    }
}
