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

            _collection.DeleteMany(new BsonDocument());
        }
    }
}
