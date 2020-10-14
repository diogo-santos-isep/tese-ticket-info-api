namespace BLL.RabbitMQ.Producers.Bodies
{
    using Models.Domain.Enums;
    using Models.Domain.Models;
    using System;

    [Serializable]
    public class TicketStateChangedEventBody : RabbitMQMessageBody
    {
        public string Ticket_Id { get; set; }
        public string Ticket_Code { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string User_Id { get; set; }
        public string User_Name { get; set; }
        public ETicketState NewState { get; set; }

        public static TicketStateChangedEventBody BuildMessage(Ticket model, User user = null)
        {
            return new TicketStateChangedEventBody
            {
                Ticket_Id = model.Id ?? throw new Exception("Ticket id is null"),
                Ticket_Code = model.Code,
                User_Id = user?.Id,
                User_Name = user?.Name,
                NewState = model.State,
            };
        }
    }
}
