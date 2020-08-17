namespace Infrastructure.CrossCutting.Settings.Implementations
{
    using Infrastructure.CrossCutting.Settings.Interfaces;
    public class MongoDBConnection : IMongoDBConnection
    {
        public string Database { get; set; }
        public string ConnectionString { get; set; }
    }
}
