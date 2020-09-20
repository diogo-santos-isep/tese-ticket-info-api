namespace BLL.RabbitMQ.Producers.Interfaces
{
    using BLL.RabbitMQ.Producers.Bodies;
    using System.Threading.Tasks;

    public interface IRabbitMQProducer<T> where T:RabbitMQMessageBody
    {
        Task Produce(T message);
    }
}