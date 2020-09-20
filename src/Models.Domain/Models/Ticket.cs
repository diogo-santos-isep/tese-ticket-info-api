namespace Models.Domain.Models
{
    using global::Models.Domain.Enums;
    using Infrastructure.CrossCutting.Helpers;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System;
    using System.Collections.Generic;

    public class Ticket : IMongoEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Code { get; set; }
        public string Subject { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public ETicketState State { get; set; }
        public int Priority { get; set; } = 3;
        [BsonRepresentation(BsonType.ObjectId)]
        public string CollaboratorId { get; set; }
        public string CollaboratorName { get; set; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; }
        public string CategoryDescription { get; set; }
        public string CategoryColor { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string DepartmentId { get; set; }
        public string DepartmentDescription { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Ticket ticket))
                return false;

            return Id == ticket.Id &&
                   Code == ticket.Code &&
                   Subject == ticket.Subject &&
                   Description == ticket.Description &&
                   State == ticket.State &&
                   Priority == ticket.Priority &&
                   CollaboratorId == ticket.CollaboratorId &&
                   CollaboratorName == ticket.CollaboratorName &&
                   ClientId == ticket.ClientId &&
                   ClientName == ticket.ClientName &&
                   ClientEmail == ticket.ClientEmail &&
                   CategoryId == ticket.CategoryId &&
                   Date.IsEqualDateTime(ticket.Date) &&
                   CategoryDescription == ticket.CategoryDescription &&
                   DepartmentId == ticket.DepartmentId &&
                   DepartmentDescription == ticket.DepartmentDescription &&
                   CategoryColor == ticket.CategoryColor;
        }

        public override int GetHashCode()
        {
            int hashCode = -1851001084;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Code);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Subject);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + EqualityComparer<ETicketState>.Default.GetHashCode(State);
            hashCode = hashCode * -1521134295 + Priority.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CollaboratorId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CollaboratorName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ClientId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ClientName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ClientEmail);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DepartmentId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DepartmentDescription);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CategoryId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CategoryDescription);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CategoryColor);
            return hashCode;
        }
        public bool StateIsDifferent(Ticket model) => this.State != model.State;

        public Ticket AssignCollaborator(User collaborator)
        {
            this.CollaboratorId = collaborator.Id;
            this.CollaboratorName = collaborator.Name;
            this.DepartmentId = collaborator.Department_Id;
            this.DepartmentDescription = collaborator.Department_Description;
            State = ETicketState.Assigned;

            return this;
        }

        public static Ticket GenerateFromMessage(string message)
        {
            return new Ticket
            {
                Subject = "Criado a partir de mensagem",
                Description = message.Substring(0,100)
            };
        }
    }
}
