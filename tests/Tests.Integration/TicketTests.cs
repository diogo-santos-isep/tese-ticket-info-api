namespace Tests.Integration
{
    using AutoFixture;
    using BLL.RabbitMQ.Producers.Bodies;
    using BLL.RabbitMQ.Producers.Interfaces;
    using BLL.Services.Implementations;
    using BLL.Services.Interfaces;
    using DAL.Repositories.Implementations;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.Domain.Enums;
    using Models.Domain.Models;
    using Models.DTO.Grids;
    using Models.Filters;
    using Moq;
    using System;
    using System.Linq;
    using Tests.Integration.Helpers;

    [TestClass]
    public class TicketTests
    {
        private TicketService _service;
        private Fixture fixture = new Fixture();
        private Mock<ITicketStateChangedEventProducer> ticketStateChangedProducerMock = new Mock<ITicketStateChangedEventProducer>();
        private Mock<ITicketCreatedEventProducer> ticketCreatedProducerMock = new Mock<ITicketCreatedEventProducer>();
        private Mock<ITicketReassignService> ticketReassignServiceMock = new Mock<ITicketReassignService>();
        private Mock<ITicketFieldsUpdatedEventProducer> ticketFieldsUpdatedServiceMock = new Mock<ITicketFieldsUpdatedEventProducer>();

        public TicketTests()
        {
            var repo = new TicketRepository(DatabaseConnection.Current.Database);
            this._service = new TicketService(repo, ticketStateChangedProducerMock.Object, ticketReassignServiceMock.Object
                , ticketCreatedProducerMock.Object, ticketFieldsUpdatedServiceMock.Object);
        }

        [TestMethod()]
        public void Create_Success()
        {
            var newTicket = this.fixture.GenerateTicket();

            newTicket = this._service.Create(newTicket);
            ticketCreatedProducerMock.Verify(x => x.Produce(It.IsAny<TicketCreatedEventBody>()), Times.Once);
            ticketReassignServiceMock.Verify(x => x.AssignTicket(It.IsAny<Ticket>()),Times.Once);
            Assert.IsFalse(String.IsNullOrEmpty(newTicket.Id), "Id came back null or empty");

            var savedTicket = this._service.Get(newTicket.Id);
            Assert.AreEqual(savedTicket, newTicket, "Tickets are different");
        }

        [TestMethod()]
        public void Create_FromMessage_Success()
        {
            var message = this.fixture.Create<string>();

            var newTicket = this._service.Create(message);
            ticketCreatedProducerMock.Verify(x => x.Produce(It.IsAny<TicketCreatedEventBody>()), Times.Once);
            ticketReassignServiceMock.Verify(x => x.AssignTicket(It.IsAny<Ticket>()), Times.Once);
            Assert.IsFalse(String.IsNullOrEmpty(newTicket.Id), "Id came back null or empty");

            var savedTicket = this._service.Get(newTicket.Id);
            Assert.AreEqual(savedTicket, newTicket, "Tickets are different");

            var generatedTicket = Ticket.GenerateFromMessage(message);
            Assert.AreEqual(generatedTicket.Subject, savedTicket.Subject);
            Assert.AreEqual(generatedTicket.Description, savedTicket.Description);
        }

        [TestMethod()]
        public void Create_NextCode_Success()
        {
            var newTicket = this.fixture.GenerateTicket();

            newTicket = this._service.Create(newTicket);

            var newTicket2 = this.fixture.GenerateTicket();
            newTicket2 = this._service.Create(newTicket2);

            var code1 = Int32.Parse(newTicket.Code.Split('-')[1]);
            var code2 = Int32.Parse(newTicket2.Code.Split('-')[1]);

            Assert.AreEqual(code1 + 1, code2, "Code is not the next");
        }

        [TestMethod()]
        public void Delete_Success()
        {
            var newTicket = this.fixture.GenerateTicket();

            newTicket = this._service.Create(newTicket);

            this._service.Delete(newTicket.Id);
            var savedTicket = this._service.Get(newTicket.Id);
            Assert.IsNull(savedTicket, "Ticket is not null, therefore still exists");
        }
        [TestMethod()]
        public void Search_Success()
        {
            var newTicket = this.fixture.GenerateTicket();
            var newTicket2 = this.fixture.GenerateTicket();

            this._service.Create(newTicket);
            this._service.Create(newTicket2);

            var grid = this._service.Search(new TicketFilter
            {
                Page = 1,
                PageSize = 50
            });
            Assert.IsTrue(grid.List.Any(t => t.Id == newTicket.Id), $"Ticket1 does not exist");
            Assert.IsTrue(grid.List.Any(t => t.Id == newTicket2.Id), $"Ticket2 does not exist");
        }

        [TestMethod()]
        public void Search_Descending_Success()
        {
            var newTicket = this.fixture.GenerateTicket();
            var newTicket2 = this.fixture.GenerateTicket();
            newTicket2.CollaboratorId = newTicket.CollaboratorId;

            this._service.Create(newTicket);
            this._service.Create(newTicket2);

            var grid = this._service.Search(new TicketFilter
            {
                Page = 1,
                PageSize = 50,
                SortBy = "Date",
                CollaboratorId = newTicket.CollaboratorId,
                SortAscending = false,
            });
            Assert.AreEqual(newTicket2.Id, grid.List.ElementAt(0).Id, $"Ticket2 does not exist on first position");
            Assert.AreEqual(newTicket.Id, grid.List.ElementAt(1).Id, $"Ticket1 does not exist on second position");
        }

        [TestMethod()]
        public void SearchForClient_Success()
        {
            var newTicket = this.fixture.GenerateTicket();
            var newTicket2 = this.fixture.GenerateTicket();

            this._service.Create(newTicket);
            this._service.Create(newTicket2);

            TicketClientVMGrid grid = this._service.SearchForClient(new TicketFilter
            {
                Page = 1,
                PageSize = 50,
                ClientId = newTicket.ClientId
            });
            Assert.AreEqual(1, grid.Count, "Número de itens não é 1");
            Assert.IsTrue(grid.List.Any(t => t.Id == newTicket.Id), $"Ticket1 does not exist");
            Assert.IsFalse(grid.List.Any(t => t.Id == newTicket2.Id), $"Ticket2 exists");
        }

        [TestMethod()]
        public void GetSingle_Success()
        {
            var newTicket = this.fixture.GenerateTicket();

            newTicket = this._service.Create(newTicket);
            Assert.IsFalse(String.IsNullOrEmpty(newTicket.Id), "Id came back null or empty");

            var savedTicket = this._service.Get(newTicket.Id);
            Assert.AreEqual(savedTicket, newTicket, "Tickets are different");
        }

        [TestMethod()]
        public void GetAll_Success()
        {
            var newTicket = this.fixture.GenerateTicket();
            var newTicket2 = this.fixture.GenerateTicket();

            this._service.Create(newTicket);
            this._service.Create(newTicket2);

            var tickets = this._service.Get();
            Assert.IsTrue(tickets.Any(t => t.Id == newTicket.Id), $"Ticket1 does not exist");
            Assert.IsTrue(tickets.Any(t => t.Id == newTicket.Id), $"Ticket2 does not exist");
        }

        [TestMethod()]
        public void Update_Success()
        {
            var newTicket = this.fixture.GenerateTicket();

            newTicket = this._service.Create(newTicket);

            newTicket.Description += "wlelele";
            this._service.Update(newTicket.Id, newTicket);
            ticketStateChangedProducerMock.Verify(x => x.Produce(It.IsAny<TicketStateChangedEventBody>()), Times.Never);
            ticketFieldsUpdatedServiceMock.Verify(x => x.Produce(It.IsAny<TicketFieldsUpdatedEventBody>()), Times.Once);

            var savedTicket = this._service.Get(newTicket.Id);
            Assert.AreEqual(savedTicket.Description, newTicket.Description, "Tickets are different");
        }

        [TestMethod()]
        public void Update_WithStateChanges_Success()
        {
            var newTicket = this.fixture.GenerateTicket();

            newTicket = this._service.Create(newTicket);

            newTicket.Description += "wlelele";
            newTicket.State = newTicket.State == ETicketState.Assigned ? ETicketState.Created : ETicketState.Assigned;
            this._service.Update(newTicket.Id, newTicket);
            ticketStateChangedProducerMock.Verify(x => x.Produce(It.IsAny<TicketStateChangedEventBody>()), Times.Once);

            var savedTicket = this._service.Get(newTicket.Id);
            Assert.AreEqual(savedTicket.Description, newTicket.Description, "Tickets are different");
        }

    }
}
