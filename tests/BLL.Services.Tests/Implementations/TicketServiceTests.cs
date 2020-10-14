namespace BLL.Services.Implementations.Tests
{
    using AutoFixture;
    using BLL.RabbitMQ.Producers.Bodies;
    using BLL.RabbitMQ.Producers.Interfaces;
    using BLL.Services.Interfaces;
    using DAL.Repositories.Interfaces;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.Domain.Enums;
    using Models.Domain.Models;
    using Models.Filters;
    using MongoDB.Bson;
    using Moq;
    using System;
    using System.Collections.Generic;

    [TestClass()]
    public class TicketServiceTests
    {
        private Mock<ITicketRepository> repoMock;
        private TicketService target;
        private Fixture fixture = new Fixture();
        private Mock<ITicketStateChangedEventProducer> ticketStateChangedProducerMock = new Mock<ITicketStateChangedEventProducer>();
        private Mock<ITicketCreatedEventProducer> ticketCreatedProducerMock = new Mock<ITicketCreatedEventProducer>();
        private Mock<ITicketReassignService> ticketReassignServiceMock = new Mock<ITicketReassignService>();
        private Mock<ITicketFieldsUpdatedEventProducer> ticketFieldsUpdatedServiceMock = new Mock<ITicketFieldsUpdatedEventProducer>();

        public TicketServiceTests()
        {
            this.repoMock = new Mock<ITicketRepository>();
            this.target = new TicketService(this.repoMock.Object, ticketStateChangedProducerMock.Object, ticketReassignServiceMock.Object
                , ticketCreatedProducerMock.Object, ticketFieldsUpdatedServiceMock.Object);
        }

        [TestMethod()]
        [TestCategory("Unit")]
        public void Create_Success()
        {
            var newTicket = this.GenerateTicket();

            this.repoMock.Setup(x => x.Create(newTicket)).Returns(newTicket);
            this.target.Create(newTicket);
            ticketCreatedProducerMock.Verify(x => x.Produce(It.IsAny<TicketCreatedEventBody>()), Times.Once);
            ticketReassignServiceMock.Verify(x => x.AssignTicket(It.IsAny<Ticket>()), Times.Once);
            Assert.IsFalse(String.IsNullOrEmpty(newTicket.Id), "Id came back null or empty");

            this.repoMock.Verify(x => x.Create(newTicket), Times.Once, "Create não foi chamado");
        }

        [TestMethod()]
        [TestCategory("Unit")]
        public void Delete_Success()
        {
            var newTicket = this.GenerateTicket();

            this.target.Delete(newTicket.Id);
            this.repoMock.Verify(x => x.Delete(newTicket.Id), Times.Once, "Delete não foi chamado");
        }

        [TestMethod()]
        [TestCategory("Unit")]
        public void Get_Success()
        {
            var id = this.fixture.Create<string>();
            this.target.Get(id);

            this.repoMock.Verify(x => x.Get(id), Times.Once, "Get não foi chamado");
        }

        [TestMethod()]
        [TestCategory("Unit")]
        public void SearchForClient_Success()
        {
            var newTicket = this.GenerateTicket();
            var filter = this.fixture.Create<TicketFilter>();
            this.repoMock.Setup(x => x.Search(filter)).Returns(new List<Ticket>() { newTicket });
            this.target.SearchForClient(filter);

            this.repoMock.Verify(x => x.Search(filter), Times.Once, "Search não foi chamado");
            this.repoMock.Verify(x => x.Count(filter), Times.Once, "Count não foi chamado");
        }

        [TestMethod()]
        [TestCategory("Unit")]
        public void Search_Success()
        {
            var newTicket = this.GenerateTicket();
            var filter = this.fixture.Create<TicketFilter>();
            this.target.Search(filter);

            this.repoMock.Verify(x => x.Search(filter), Times.Once, "Search não foi chamado");
            this.repoMock.Verify(x => x.Count(filter), Times.Once, "Count não foi chamado");
        }

        [TestMethod()]
        [TestCategory("Unit")]
        public void Update_Success()
        {
            var newTicket = this.GenerateTicket();
            var newTicket2 = this.GenerateTicket();
            newTicket2.State = newTicket.State;

            this.repoMock.Setup(x => x.Get(newTicket.Id)).Returns(newTicket2);
            this.target.Update(newTicket.Id, newTicket);
            ticketStateChangedProducerMock.Verify(x => x.Produce(It.IsAny<TicketStateChangedEventBody>()), Times.Never);
            ticketFieldsUpdatedServiceMock.Verify(x => x.Produce(It.IsAny<TicketFieldsUpdatedEventBody>()), Times.Once);

            this.repoMock.Verify(x => x.Update(newTicket.Id, newTicket), Times.Once, "Search não foi chamado");
        }

        [TestMethod()]
        [TestCategory("Unit")]
        public void Update_WithStateChanges_Success()
        {
            var newTicket = this.GenerateTicket();
            newTicket.State = ETicketState.Assigned;
            var newTicket2 = this.GenerateTicket();
            newTicket.State = ETicketState.BeingHandled;

            this.repoMock.Setup(x => x.Get(newTicket.Id)).Returns(newTicket2);
            this.target.Update(newTicket.Id, newTicket);
            ticketStateChangedProducerMock.Verify(x => x.Produce(It.IsAny<TicketStateChangedEventBody>()), Times.Once);

            this.repoMock.Verify(x => x.Update(newTicket.Id, newTicket), Times.Once, "Search não foi chamado");
        }

        public Ticket GenerateTicket()
        {
            return this.fixture.Build<Ticket>()
                .With(t => t.CollaboratorId, ObjectId.GenerateNewId().ToString())
                .With(t => t.DepartmentId, ObjectId.GenerateNewId().ToString())
                .With(t => t.CategoryId, ObjectId.GenerateNewId().ToString())
                .With(t => t.ClientId, ObjectId.GenerateNewId().ToString())
                .Create();
        }
    }
}