namespace BLL.RabbitMQ.Producers.Bodies
{
    using Models.Domain.Models;
    using System;

    [Serializable]
    public class TicketReassignedEventBody : RabbitMQMessageBody
    {
        public string Ticket_Id { get; set; }
        public string Ticket_Code { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string User_Id { get; set; }
        public string User_Name { get; set; }
        public string NewDepartment_Id { get; set; }
        public string NewDepartment_Description { get; set; }
        public string NewCollaborator_Id { get; set; }
        public string NewCollaborator_Name { get; set; }

        public static TicketReassignedEventBody BuildMessage(Ticket model, User user = null)
        {
            return new TicketReassignedEventBody
            {
                Ticket_Id = model.Id,
                Ticket_Code = model.Code,
                User_Id = user?.Id,
                User_Name = user?.Name,
                NewDepartment_Id = model.DepartmentId,
                NewDepartment_Description = model.DepartmentDescription,
                NewCollaborator_Id = model.CollaboratorId,
                NewCollaborator_Name = model.CollaboratorName
            };
        }
    }
}
