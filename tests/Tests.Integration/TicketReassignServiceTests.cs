namespace Tests.Integration
{
    using AutoFixture;
    using BLL.RabbitMQ.Producers.Bodies;
    using BLL.RabbitMQ.Producers.Interfaces;
    using BLL.Services.Implementations;
    using BLL.Services.Interfaces;
    using DAL.Clients.Interfaces;
    using DAL.Repositories.Implementations;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.Domain.Enums;
    using Models.Domain.Models;
    using Moq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Tests.Integration.Helpers;

    [TestClass]
    public class TicketReassignServiceTests
    {
        private TicketReassignService _service;
        private TicketService _ticketService;
        private Fixture fixture = new Fixture();
        private Mock<ITicketReassignedEventProducer> ticketReassignedEventProducerMock = new Mock<ITicketReassignedEventProducer>();
        private Mock<ITicketStateChangedEventProducer> ticketStateChangedEventProducerMock = new Mock<ITicketStateChangedEventProducer>();
        private Mock<IDepartmentClient> departmentClientMock = new Mock<IDepartmentClient>();
        private Mock<IUserClient> userClientMock = new Mock<IUserClient>();

        private Mock<ITicketCreatedEventProducer> ticketCreatedProducerMock = new Mock<ITicketCreatedEventProducer>();
        private Mock<ITicketReassignService> ticketReassignServiceMock = new Mock<ITicketReassignService>();
        private Mock<ITicketFieldsUpdatedEventProducer> ticketFieldsUpdatedServiceMock = new Mock<ITicketFieldsUpdatedEventProducer>();

        public TicketReassignServiceTests()
        {
            var repo = new TicketRepository(DatabaseConnection.Current.Database);
            this._service = new TicketReassignService(departmentClientMock.Object, userClientMock.Object, repo
                , ticketReassignedEventProducerMock.Object, ticketStateChangedEventProducerMock.Object);
            this._ticketService = new TicketService(repo, ticketStateChangedEventProducerMock.Object, ticketReassignServiceMock.Object
                , ticketCreatedProducerMock.Object, ticketFieldsUpdatedServiceMock.Object);
        }

        [TestMethod]
        public async Task AssignTicket_Success()
        {
            var ticket1 = this.fixture.GenerateTicket();
            ticket1.State = ETicketState.Created;
            ticket1 = this._ticketService.Create(ticket1);

            var collaborator = this.fixture.GenerateUser();
            this.userClientMock.Setup(x => x.GetCollaboratorsFromDepartment(It.IsAny<Department>())).ReturnsAsync(new List<User> { collaborator });

            await this._service.AssignTicket(ticket1).ConfigureAwait(false);
            this.ticketReassignedEventProducerMock.Verify(x => x.Produce(It.IsAny<TicketReassignedEventBody>()), Times.Once);
            this.ticketStateChangedEventProducerMock.Verify(x => x.Produce(It.IsAny<TicketStateChangedEventBody>()), Times.Once);

            var updatedTicket = this._ticketService.Get(ticket1.Id);
            Assert.AreEqual(collaborator.Id, updatedTicket.CollaboratorId, "Collaborator is not the same");
            Assert.AreEqual(collaborator.Name, updatedTicket.CollaboratorName, "Collaborator is not the same");
            Assert.AreEqual(collaborator.Department_Id, updatedTicket.DepartmentId, "Department is not the same");
            Assert.AreEqual(collaborator.Department_Description, updatedTicket.DepartmentDescription, "Collaborator is not the same");
        }
    }
}
