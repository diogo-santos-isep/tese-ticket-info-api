using Infrastructure.CrossCutting;
using Infrastructure.CrossCutting.Settings.Interfaces;
using MongoDB.Driver;
using System.Runtime.CompilerServices;

namespace Tests.Integration.Helpers
{
    public class DatabaseConnection
    {
        public static DatabaseConnection Current { get; private set; }
        public IMongoDatabase Database { get; }

        public DatabaseConnection(IMongoDatabase mongoDatabase)
        {
            this.Database = mongoDatabase;
        }

        public static void Init(IMongoDBConnection connectionSettings)
        {
            Current = new DatabaseConnection(MongoDBConnecter.Connect(connectionSettings));
        }
    }
}
