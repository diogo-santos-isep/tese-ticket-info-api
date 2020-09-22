namespace BLL.RabbitMQ.Producers.Bodies
{
    using Models.Domain.Models;
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class TicketFieldsUpdatedEventBody : RabbitMQMessageBody
    {
        public string Ticket_Id { get; set; }
        public string Ticket_Code { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string User_Id { get; set; }
        public string User_Name { get; set; }
        public IEnumerable<string> ChangedFields { get; set; }

        public static TicketFieldsUpdatedEventBody BuildMessage(Ticket model, IEnumerable<string> changedFields)
        {
            return new TicketFieldsUpdatedEventBody
            {
                Ticket_Id = model.Id,
                Ticket_Code = model.Code,
                Date = model.Date,
                User_Id = model.CollaboratorId,
                User_Name= model.CollaboratorName,
                ChangedFields = changedFields
            };
        }
    }
}
