namespace Presentation.API.Components
{
    using BLL.RabbitMQ.Producers.Implementations;
    using BLL.RabbitMQ.Producers.Interfaces;
    using Infrastructure.CrossCutting.Settings.Implementations;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    public static class RabbitComponents
    {
        public static IServiceCollection AddRabbitMQProducers(this IServiceCollection services)
        {
            services.AddScoped<ITicketStateChangedEventProducer>(p => new TicketStateChangedEventProducer(p.GetService<IOptions<RabbitMQSettings>>().Value));
            services.AddScoped<ITicketCreatedEventProducer>(p => new TicketCreatedEventProducer(p.GetService<IOptions<RabbitMQSettings>>().Value));
            services.AddScoped<ITicketReassignedEventProducer>(p => new TicketReassignedEventProducer(p.GetService<IOptions<RabbitMQSettings>>().Value));
            services.AddScoped<ITicketFieldsUpdatedEventProducer>(p => new TicketFieldsUpdatedEventProducer(p.GetService<IOptions<RabbitMQSettings>>().Value));

            return services;
        }
    }
}
