using Models.Domain.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Integration.Helpers
{
    public class DbCleaner
    {
        internal static void Clean()
        {
            var _collection = DatabaseConnection.Current.Database.GetCollection<Ticket>("Tickets");
            var _collectionNotes = DatabaseConnection.Current.Database.GetCollection<Ticket>("TicketNotes");

            _collection.DeleteMany(new BsonDocument());
            _collectionNotes.DeleteMany(new BsonDocument());
        }
    }
}
