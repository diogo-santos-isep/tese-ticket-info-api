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
        public static Department GenerateDepartment(this Fixture fixture)
        {
            return fixture.Build<Department>()
                .Without(t => t.Id)
                .Create();
        }
        public static TicketNote GenerateTicketNote(this Fixture fixture)
        {
            return fixture.Build<TicketNote>()
                .Without(t => t.Id)
                .With(t => t.User_Id, ObjectId.GenerateNewId().ToString())
                .With(t => t.Ticket_Id, ObjectId.GenerateNewId().ToString())
                .Create();
        }

    }
}
