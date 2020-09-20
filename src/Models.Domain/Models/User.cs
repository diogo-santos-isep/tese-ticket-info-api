namespace Models.Domain.Models
{
    using global::Models.Domain.Enums;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System;
    using System.Collections.Generic;

    public class User : IMongoEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; } = true;
        public string Password { get; set; }
        public string Photo { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string Department_Id { get; set; }
        public string Department_Description { get; set; }

        public ERole Role { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is User user))
                return false;
            return Id == user.Id &&
                   Name == user.Name &&
                   Email == user.Email &&
                   Photo == user.Photo &&
                   Password == user.Password &&
                   Department_Id == user.Department_Id &&
                   Department_Description == user.Department_Description &&
                   Role == user.Role;
        }

        public override int GetHashCode()
        {
            int hashCode = 993565665;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Photo);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Password);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Department_Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Department_Description);
            hashCode = hashCode * -1521134295 + Role.GetHashCode();
            return hashCode;
        }

        public static User GetAdminDefault()
        {
            return new User
            {
                Email = "admin@admin.pt",
                Password = "admin",
                Name = "Default Administrator",
                Role = ERole.Admin
            };
        }
    }
}
