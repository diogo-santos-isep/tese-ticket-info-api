namespace DAL.RabbitMQ.Producers.Interfaces
{
    using DAL.RabbitMQ.Producers.Bodies;
    using System.Threading.Tasks;

    public interface IRabbitMQProducer<T> where T:RabbitMQMessageBody
    {
        Task Produce(T message);
    }
}