namespace Models.Domain.Models
{
    using global::Models.Domain.Enums;
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
        public string Description { get; set; }
        public string Category { get; set; }
        public ETicketState State { get; set; }
        public int Priority { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string CollaboratorId { get; set; }
        public string CollaboratorName { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string ClientId { get; set; }

        public bool StateIsDifferent(Ticket model) => this.State != model.State;

        public string ClientName { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; }
        public string CategoryDescription { get; set; }
        public string CategoryColor { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Ticket ticket &&
                   Id == ticket.Id &&
                   Code == ticket.Code &&
                   Subject == ticket.Subject &&
                   Description == ticket.Description &&
                   Category == ticket.Category &&
                   State == ticket.State &&
                   Priority == ticket.Priority &&
                   CollaboratorId == ticket.CollaboratorId &&
                   CollaboratorName == ticket.CollaboratorName &&
                   ClientId == ticket.ClientId &&
                   ClientName == ticket.ClientName &&
                   CategoryId == ticket.CategoryId &&
                   CategoryDescription == ticket.CategoryDescription &&
                   CategoryColor == ticket.CategoryColor;
        }

        public override int GetHashCode()
        {
            int hashCode = -1851001084;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Code);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Subject);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Category);
            hashCode = hashCode * -1521134295 + EqualityComparer<ETicketState>.Default.GetHashCode(State);
            hashCode = hashCode * -1521134295 + Priority.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CollaboratorId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CollaboratorName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ClientId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ClientName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CategoryId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CategoryDescription);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CategoryColor);
            return hashCode;
        }
    }
}
