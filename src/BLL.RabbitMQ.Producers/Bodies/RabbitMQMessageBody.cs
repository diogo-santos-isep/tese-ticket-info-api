namespace BLL.RabbitMQ.Producers.Bodies
{
    using System;
    [Serializable]
    public abstract class RabbitMQMessageBody
    {
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
