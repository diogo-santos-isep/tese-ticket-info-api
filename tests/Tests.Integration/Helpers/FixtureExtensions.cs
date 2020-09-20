namespace Tests.Integration.Helpers
{
    using AutoFixture;
    using Models.Domain.Models;
    using MongoDB.Bson;

    public static class FixtureExtensions
    {
        public static User GenerateUser(this Fixture fixture)
        {
            return fixture.Build<User>()
                .Without(t => t.Id)
                .With(t => t.Department_Id, ObjectId.GenerateNewId().ToString())
                .Create();
        }
        public static Ticket GenerateTicket(this Fixture fixture)
        {
            return fixture.Build<Ticket>()
                .Without(t => t.Id)
                .With(t => t.CollaboratorId, ObjectId.GenerateNewId().ToString())
                .With(t => t.DepartmentId, ObjectId.GenerateNewId().ToString())
                .With(t => t.CategoryId, ObjectId.GenerateNewId().ToString())
                .With(t => t.ClientId, ObjectId.GenerateNewId().ToString())
                .Create();
        }

    }
}
