using System;

namespace DAL.RabbitMQ.Producers.Bodies
{
    [Serializable]
    public abstract class RabbitMQMessageBody
    {
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
