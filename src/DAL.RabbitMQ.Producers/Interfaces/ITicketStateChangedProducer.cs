namespace DAL.RabbitMQ.Producers.Interfaces
{
    using DAL.RabbitMQ.Producers.Bodies;
    public interface ITicketStateChangedProducer : IRabbitMQProducer<TicketStateChangedBody>
    {
        
    }
}
