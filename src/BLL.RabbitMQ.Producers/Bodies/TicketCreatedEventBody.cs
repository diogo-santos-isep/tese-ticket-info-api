namespace BLL.RabbitMQ.Producers.Bodies
{
    using Models.Domain.Models;
    using System;

    [Serializable]
    public class TicketCreatedEventBody : RabbitMQMessageBody
    {
        public string Ticket_Id { get; set; }
        public string Ticket_Code { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string User_Id { get; set; }
        public string User_Name { get; set; }

        public static TicketCreatedEventBody BuildMessage(Ticket model)
        {
            return new TicketCreatedEventBody
            {
                Ticket_Id = model.Id,
                Ticket_Code = model.Code,
                Date = model.Date,
                User_Id = model.ClientId,
                User_Name= model.ClientName
            };
        }
    }
}
