namespace Presentation.API.Components
{
    using BLL.Services.Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    public static class ServiceComponents
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITicketService>();

            return services;
        }
    }
}
