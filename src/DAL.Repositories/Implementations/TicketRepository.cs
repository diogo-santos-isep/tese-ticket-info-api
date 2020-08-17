namespace DAL.Repositories.Implementations
{
    using DAL.Repositories.Interfaces;
    using Models.Domain.Models;
    using MongoDB.Driver;
    using System.Collections.Generic;

    public class TicketRepository : ITicketRepository
    {
        private readonly string COLLECTIONNAME = "Tickets";
        private readonly IMongoCollection<Ticket> _collection;

        public TicketRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<Ticket>(COLLECTIONNAME);
        }
        public Ticket Create(Ticket model)
        {
            _collection.InsertOne(model);
            return model;
        }

        public void Delete(string id) =>
            _collection.DeleteOne(book => book.Id == id);

        public List<Ticket> Get() =>
            _collection.Find(book => true).ToList();

        public Ticket Get(string id) =>
            _collection.Find<Ticket>(book => book.Id == id).FirstOrDefault();

        public void Update(string id, Ticket model) =>
            _collection.ReplaceOne(book => book.Id == id, model);
    }
}
