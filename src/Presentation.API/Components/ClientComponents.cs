namespace Presentation.API.Components
{
    using DAL.Clients.Implementations;
    using DAL.Clients.Interfaces;
    using Infrastructure.CrossCutting.Settings.Implementations;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    public static class ClientComponents
    {
        public static IServiceCollection AddClients(this IServiceCollection services)
        {
            services.AddHttpClient<IUserClient, UserClient>();
            services.AddHttpClient<IDepartmentClient, DepartmentClient>();

            services.AddSingleton<TokenClient>();
            //services.AddSingleton<TokenClient>(p=>new TokenClient(p.GetRequiredService<IOptions<AuthenticationSettings>>().Value));

            return services;
        }
    }
}
