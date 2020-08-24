namespace DAL.RabbitMQ.Producers.Bodies
{
    using Models.Domain.Enums;
    using Models.Domain.Models;
    using System;

    [Serializable]
    public class TicketStateChangedBody : RabbitMQMessageBody
    {
        public string TicketId { get; set; }
        public ETicketState NewState { get; set; }

        public static TicketStateChangedBody BuildMessage(Ticket model)
        {
            return new TicketStateChangedBody
            {
                TicketId = model.Id ?? throw new Exception("Ticket id is null"),
                NewState = model.State,
            };
        }
    }
}
