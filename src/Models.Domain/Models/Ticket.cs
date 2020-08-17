namespace Models.Domain.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
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
    }
}
