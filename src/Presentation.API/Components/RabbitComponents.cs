namespace Presentation.API.Components
{
    using DAL.RabbitMQ.Producers.Implementations;
    using DAL.RabbitMQ.Producers.Interfaces;
    using Infrastructure.CrossCutting.Settings.Implementations;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    public static class RabbitComponents
    {
        public static IServiceCollection AddRabbitMQProducers(this IServiceCollection services)
        {
            services.AddScoped<ITicketStateChangedProducer>(p => new TicketStateChangedProducer(p.GetService<IOptions<RabbitMQSettings>>().Value));

            return services;
        }
    }
}
