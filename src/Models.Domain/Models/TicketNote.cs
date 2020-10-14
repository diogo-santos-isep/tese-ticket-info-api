namespace Models.Domain.Models
{
    using Infrastructure.CrossCutting.Helpers;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System;
    using System.Collections.Generic;

    public class TicketNote : IMongoEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Description { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Date { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string Ticket_Id { get; set; }
        public string User_Id { get; set; }
        public string User_Name { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is TicketNote note))
                return false;
            return Id == note.Id &&
                   Description == note.Description &&
                   Date.IsEqualDateTime(note.Date) &&
                   Ticket_Id == note.Ticket_Id &&
                   User_Id == note.User_Id &&
                   User_Name == note.User_Name;
        }

        public override int GetHashCode()
        {
            int hashCode = 266267163;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + Date.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Ticket_Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(User_Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(User_Name);
            return hashCode;
        }
    }
}
