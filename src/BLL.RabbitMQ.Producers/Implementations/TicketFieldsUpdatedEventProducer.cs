namespace BLL.RabbitMQ.Producers.Implementations
{
    using BLL.RabbitMQ.Producers.Bodies;
    using BLL.RabbitMQ.Producers.Extensions;
    using BLL.RabbitMQ.Producers.Helpers;
    using BLL.RabbitMQ.Producers.Interfaces;
    using global::RabbitMQ.Client;
    using Infrastructure.CrossCutting.Settings.Implementations;
    using Newtonsoft.Json;
    using System;
    using System.Text;
    using System.Threading.Tasks;

    public class TicketFieldsUpdatedEventProducer : ITicketFieldsUpdatedEventProducer
    {
        private readonly string QUEUENAME = "TicketFieldsUpdatedQueue";
        private IConnectionFactory factory;

        public TicketFieldsUpdatedEventProducer(RabbitMQSettings settings)
        {
            this.factory = settings.ToFactory();
        }

        public async Task Produce(TicketFieldsUpdatedEventBody message)
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

                    Console.WriteLine($"TicketFieldsUpdatedEvent Message published with success!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something Went Wrong Publishing TicketFieldsUpdatedEvent! {ex.Message}");
            }
        }
    }
}
