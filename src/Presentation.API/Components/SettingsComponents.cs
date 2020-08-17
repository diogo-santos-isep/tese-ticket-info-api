namespace Presentation.API
{
    using Infrastructure.CrossCutting.Settings.Implementations;
    using Infrastructure.CrossCutting.Settings.Interfaces;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;
    using Presentation.API.Helpers;

    public static class SettingsComponents
    {
        public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<IMongoDBConnection>(
    configuration.GetSection(nameof(IMongoDBConnection)));

            services.AddScoped<IMongoDatabase>(sp =>
            sp.GetRequiredService<IOptions<MongoDBConnection>>().Value.Connect());

            return services;
        }
    }
}
