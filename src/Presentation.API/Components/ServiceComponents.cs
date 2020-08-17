namespace Presentation.API.Components
{
    using BLL.Services.Implementations;
    using Microsoft.Extensions.DependencyInjection;
    public static class ServiceComponents
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<TicketService>();

            return services;
        }
    }
}
