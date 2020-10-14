﻿namespace BLL.RabbitMQ.Producers.Interfaces
{
    using BLL.RabbitMQ.Producers.Bodies;
    public interface ITicketCreatedEventProducer : IRabbitMQProducer<TicketCreatedEventBody>
    {
        
    }
}
