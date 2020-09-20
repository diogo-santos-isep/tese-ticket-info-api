namespace Models.Domain.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System.Collections.Generic;

    public class Department : IMongoEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Description { get; set; }
        public override bool Equals(object obj)
        {
            return obj is Department category &&
                   Id == category.Id &&
                   Description == category.Description;
        }

        public override int GetHashCode()
        {
            int hashCode = -1851001084;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            return hashCode;
        }
    }
}
