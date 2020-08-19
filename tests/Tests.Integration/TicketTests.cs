namespace Tests.Integration
{
    using AutoFixture;
    using BLL.Services.Implementations;
    using DAL.Repositories.Implementations;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.Domain.Models;
    using System;
    using System.Linq;
    using Tests.Integration.Helpers;

    [TestClass]
    public class TicketTests
    {
        private TicketService _service;
        private Fixture fixture = new Fixture();

        public TicketTests()
        {
            var repo = new TicketRepository(DatabaseConnection.Current.Database);
            this._service = new TicketService(repo);
        }

        [TestMethod()]
        public void Create_Success()
        {
            var newTicket = this.GenerateNewTicket();

            newTicket = this._service.Create(newTicket);
            Assert.IsFalse(String.IsNullOrEmpty(newTicket.Id),"Id came back null or empty");

            var savedTicket = this._service.Get(newTicket.Id);
            Assert.AreEqual(savedTicket, newTicket, "Tickets are different");
        }

        [TestMethod()]
        public void Delete_Success()
        {
            var newTicket = this.GenerateNewTicket();

            newTicket = this._service.Create(newTicket);

            this._service.Delete(newTicket.Id);
            var savedTicket = this._service.Get(newTicket.Id);
            Assert.IsNull(savedTicket,"Ticket is not null, therefore still exists");
        }

        [TestMethod()]
        public void GetSingle_Success()
        {
            var newTicket = this.GenerateNewTicket();

            newTicket = this._service.Create(newTicket);

            var savedTicket = this._service.Get(newTicket.Id);
            Assert.IsNotNull(savedTicket, "Ticket does not exist");
            Assert.AreEqual(savedTicket, newTicket, "Tickets are different");
        }

        [TestMethod()]
        public void GetAll_Success()
        {
            var newTicket = this.GenerateNewTicket();
            var newTicket2 = this.GenerateNewTicket();

             this._service.Create(newTicket);
             this._service.Create(newTicket2);

            var tickets = this._service.Get();
            Assert.IsTrue(tickets.Any(t=>t.Id == newTicket.Id), $"Ticket1 does not exist");
            Assert.IsTrue(tickets.Any(t=>t.Id == newTicket.Id), $"Ticket2 does not exist");
        }

        [TestMethod()]
        public void Update_Success()
        {
            var newTicket = this.GenerateNewTicket();

            newTicket = this._service.Create(newTicket);

            newTicket.Description += "wlelele";
            this._service.Update(newTicket.Id, newTicket);
            var savedTicket = this._service.Get(newTicket.Id);
            Assert.AreEqual(savedTicket.Description, newTicket.Description, "Tickets are different");
        }

        private Ticket GenerateNewTicket()
        {
            return this.fixture.Build<Ticket>()
                .Without(t => t.Id)
                .Create();
        }
    }
}
