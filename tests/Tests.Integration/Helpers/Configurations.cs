namespace Tests.Integration.Helpers
{
    using Infrastructure.CrossCutting.Settings.Implementations;
    using Microsoft.Extensions.Configuration;
    using System;

    public class Configurations
    {
        public static Configurations Current { get; private set; }
        public MongoDBConnection MongoDBConnection { get; }

        public Configurations(MongoDBConnection mongoDbConnection)
        {
            this.MongoDBConnection = mongoDbConnection ?? throw new Exception("Connection is null");
        }

        public static void Init()
        {
            var config = new ConfigurationBuilder()
                  .AddJsonFile("appsettings.test.json")
                  .Build();

            //TODO fix this (use config)
            var mongo = new MongoDBConnection
            {
                ConnectionString = "mongodb://localhost:27017",
                Database = "ticket-info-db-tests"
            };

            Current = new Configurations(mongo);
        }
    }
}
