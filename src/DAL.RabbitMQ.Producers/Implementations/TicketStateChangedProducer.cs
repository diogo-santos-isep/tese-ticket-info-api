namespace DAL.RabbitMQ.Producers.Implementations
{
    using DAL.RabbitMQ.Producers.Bodies;
    using DAL.RabbitMQ.Producers.Extensions;
    using DAL.RabbitMQ.Producers.Helpers;
    using DAL.RabbitMQ.Producers.Interfaces;
    using global::RabbitMQ.Client;
    using Infrastructure.CrossCutting.Settings.Implementations;
    using Newtonsoft.Json;
    using System;
    using System.Text;
    using System.Threading.Tasks;

    public class TicketStateChangedProducer : ITicketStateChangedProducer
    {
        private readonly string QUEUENAME = "TicketStateChangedQueue";
        private IConnectionFactory factory;

        public TicketStateChangedProducer(RabbitMQSettings settings)
        {
            this.factory = settings.ToFactory();
        }

        public async Task Produce(TicketStateChangedBody message)
        {
            try
            {
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: QUEUENAME,
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    byte[] body = Encoding.Default.GetBytes(JsonConvert.SerializeObject(message));

                    channel.BasicPublish(exchange: "",
                                         routingKey: QUEUENAME,
                                         basicProperties: null,
                                         body: body);

                    Console.WriteLine($"TicketStateChanged Message published with success!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something Went Wrong! {ex.Message}");
            }
        }
    }
}
