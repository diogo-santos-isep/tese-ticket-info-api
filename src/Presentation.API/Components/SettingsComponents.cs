namespace Presentation.API
{
    using Infrastructure.CrossCutting;
    using Infrastructure.CrossCutting.Settings.Implementations;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;

    public static class SettingsComponents
    {
        public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDBConnection>(
    configuration.GetSection(nameof(MongoDBConnection)));
            services.Configure<RabbitMQSettings>(
    configuration.GetSection(nameof(RabbitMQSettings)));

            services.AddScoped<IMongoDatabase>(sp =>
            sp.GetRequiredService<IOptions<MongoDBConnection>>().Value.Connect());

            return services;
        }
    }
}
