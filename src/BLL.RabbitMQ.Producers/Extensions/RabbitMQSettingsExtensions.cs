namespace BLL.RabbitMQ.Producers.Extensions
{
    using global::RabbitMQ.Client;
    using Infrastructure.CrossCutting.Settings.Implementations;

    public static class RabbitMQSettingsExtensions
    {
        public static IConnectionFactory ToFactory(this RabbitMQSettings settings)
        {
            return new ConnectionFactory
            {
                HostName = settings.Host,
                Port = settings.Port,
                UserName = settings.Username,
                Password = settings.Password
            };
        }
    }
}
