using Infrastructure.CrossCutting;
using Infrastructure.CrossCutting.Settings.Implementations;
using MongoDB.Driver;

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

        public static void Init(MongoDBConnection connectionSettings)
        {
            Current = new DatabaseConnection(MongoDBConnecter.Connect(connectionSettings));
        }
    }
}
