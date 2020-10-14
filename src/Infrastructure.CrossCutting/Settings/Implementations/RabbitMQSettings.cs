namespace Infrastructure.CrossCutting.Settings.Implementations
{
    using RabbitMQ.Client;
    public class RabbitMQSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public virtual IConnectionFactory ToFactory()
        {
            return new ConnectionFactory
            {
                HostName = Host,
                Port = Port,
                UserName = Username,
                Password = Password
            };
        }
    }
}
