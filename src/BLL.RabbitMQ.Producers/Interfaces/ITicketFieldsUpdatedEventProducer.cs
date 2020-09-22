namespace BLL.RabbitMQ.Producers.Interfaces
{
    using BLL.RabbitMQ.Producers.Bodies;
    public interface ITicketReassignedEventProducer : IRabbitMQProducer<TicketReassignedEventBody>
    {
        
    }
}
