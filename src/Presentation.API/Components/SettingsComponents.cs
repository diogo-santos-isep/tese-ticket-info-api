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
            services.Configure<MongoDBConnection>(configuration.GetSection(nameof(MongoDBConnection)));
            services.Configure<RabbitMQSettings>(configuration.GetSection(nameof(RabbitMQSettings)));
            services.Configure<UriSettings>(configuration.GetSection(nameof(UriSettings)));
            services.Configure<AuthenticationSettings>(configuration.GetSection(nameof(AuthenticationSettings)));

            services.AddSingleton(p=>p.GetRequiredService<IOptions<MongoDBConnection>>().Value);
            services.AddSingleton(p=>p.GetRequiredService<IOptions<RabbitMQSettings>>().Value);
            services.AddSingleton(p=>p.GetRequiredService<IOptions<AuthenticationSettings>>().Value);
            services.AddSingleton(p=>p.GetRequiredService<IOptions<UriSettings>>().Value);

            var removeme = services.BuildServiceProvider().GetRequiredService<AuthenticationSettings>();
            var removeme2 = services.BuildServiceProvider().GetRequiredService<UriSettings>();

            services.AddScoped<IMongoDatabase>(sp =>sp.GetRequiredService<IOptions<MongoDBConnection>>().Value.Connect());

            return services;
        }
    }
}
