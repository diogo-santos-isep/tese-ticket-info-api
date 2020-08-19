namespace Infrastructure.CrossCutting
{
    using Infrastructure.CrossCutting.Settings.Interfaces;
    using MongoDB.Driver;
    using System;

    public static  class MongoDBConnecter
    {
        public static IMongoDatabase Connect(this IMongoDBConnection settings)
        {
            try
            {
                var client = new MongoClient(settings.ConnectionString);
                var database = client.GetDatabase(settings.Database) ?? throw new Exception("Database is null");

                return database;
            }
            catch
            {
                //TODO: Log and throw?
                throw;
            }
        }
    }
}
