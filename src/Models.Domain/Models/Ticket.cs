namespace Models.Domain.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
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
        public string State { get; set; }
        public string State_Color { get; set; }
        public int Priority { get; set; }
        public string Collaborator_Id { get; set; }
        public string Collaborator_Name { get; set; }
        public string Client_Id { get; set; }
        public string Client_Name { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Ticket ticket &&
                   Id == ticket.Id &&
                   Code == ticket.Code &&
                   Subject == ticket.Subject &&
                   Description == ticket.Description &&
                   Category == ticket.Category &&
                   State == ticket.State &&
                   State_Color == ticket.State_Color &&
                   Priority == ticket.Priority &&
                   Collaborator_Id == ticket.Collaborator_Id &&
                   Collaborator_Name == ticket.Collaborator_Name &&
                   Client_Id == ticket.Client_Id &&
                   Client_Name == ticket.Client_Name;
        }

        public override int GetHashCode()
        {
            int hashCode = -1851001084;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Code);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Subject);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Category);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(State);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(State_Color);
            hashCode = hashCode * -1521134295 + Priority.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Collaborator_Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Collaborator_Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Client_Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Client_Name);
            return hashCode;
        }
    }
}
