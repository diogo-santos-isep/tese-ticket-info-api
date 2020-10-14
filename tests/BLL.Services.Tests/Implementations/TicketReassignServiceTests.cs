namespace BLL.Services.Implementations.Tests
{
    using AutoFixture;
    using BLL.RabbitMQ.Producers.Bodies;
    using BLL.RabbitMQ.Producers.Interfaces;
    using DAL.Clients.Interfaces;
    using DAL.Repositories.Interfaces;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.Domain.Enums;
    using Models.Domain.Models;
    using Models.Filters;
    using MongoDB.Bson;
    using Moq;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [TestClass()]
    public class TicketReassignServiceTests
    {
        private Mock<ITicketRepository> repoMock;
        private TicketReassignService target;
        private Fixture fixture = new Fixture();
        private Mock<ITicketReassignedEventProducer> ticketReassignedEventProducerMock = new Mock<ITicketReassignedEventProducer>();
        private Mock<ITicketStateChangedEventProducer> ticketStateChangedEventProducerMock = new Mock<ITicketStateChangedEventProducer>();
        private Mock<IDepartmentClient> departmentClientMock = new Mock<IDepartmentClient>();
        private Mock<IUserClient> userClientMock = new Mock<IUserClient>();

        public TicketReassignServiceTests()
        {
            this.repoMock = new Mock<ITicketRepository>();
            this.target = new TicketReassignService(departmentClientMock.Object, userClientMock.Object
                , repoMock.Object, ticketReassignedEventProducerMock.Object, ticketStateChangedEventProducerMock.Object);
        }

        [TestMethod()]
        [TestCategory("Unit")]
        public async Task AssignTicket_Success()
        {
            var newTicket = this.GenerateTicket();
            newTicket.State = ETicketState.Created;

            var collaborator = this.GenerateUser();
            this.userClientMock.Setup(x => x.GetCollaboratorsFromDepartment(It.IsAny<Department>())).ReturnsAsync(new List<User> { collaborator });
            this.repoMock.Setup(x => x.Search(It.IsAny<TicketFilter>())).Returns(new List<Ticket>());

            await this.target.AssignTicket(newTicket).ConfigureAwait(false);

            this.departmentClientMock.Verify(x => x.GetDefaultDepartment(), Times.Once);
            this.userClientMock.Verify(x => x.GetCollaboratorsFromDepartment(It.IsAny<Department>()), Times.Once);

            this.ticketReassignedEventProducerMock.Verify(x => x.Produce(It.IsAny<TicketReassignedEventBody>()), Times.Once);
            this.ticketStateChangedEventProducerMock.Verify(x => x.Produce(It.IsAny<TicketStateChangedEventBody>()), Times.Once);

            this.repoMock.Verify(x => x.Update(newTicket.Id, newTicket), Times.Once, "Update não foi chamado");
        }

        [TestMethod()]
        [TestCategory("Unit")]
        public void GetMostFreeCollaborator_Test1_Success()
        {
            //Setup
            var ticketP1 = this.GenerateTicket();
            var ticketP3 = this.GenerateTicket();
            var ticketP4 = this.GenerateTicket();
            ticketP1.Priority = 1;
            ticketP3.Priority = 3;
            ticketP4.Priority = 4;

            var antonio = this.GenerateUser();
            antonio.Name = "António";
            var joao = this.GenerateUser();
            joao.Name = "João";
            var maria = this.GenerateUser();
            maria.Name = "Maria";

            this.repoMock.Setup(r => r.Search(It.Is<TicketFilter>(f => f.CollaboratorId == antonio.Id)))
                .Returns(new List<Ticket>() { ticketP4 });
            this.repoMock.Setup(r => r.Search(It.Is<TicketFilter>(f => f.CollaboratorId == joao.Id)))
                .Returns(new List<Ticket>() { ticketP3 });
            this.repoMock.Setup(r => r.Search(It.Is<TicketFilter>(f => f.CollaboratorId == maria.Id)))
                .Returns(new List<Ticket>() { });

            //Act
            var result = this.target.GetMostFreeCollaborator(new List<User>() { antonio, joao, maria });

            //Assert
            Assert.AreEqual(maria.Id, result.Id, $"Colaborador mais livre é {result.Name} em vez da Maria");
        }

        [TestMethod()]
        [TestCategory("Unit")]
        public void GetMostFreeCollaborator_Test2_Success()
        {
            //Setup
            var ticketP1 = this.GenerateTicket();
            var ticketP3 = this.GenerateTicket();
            var ticketP4 = this.GenerateTicket();
            ticketP1.Priority = 1;
            ticketP3.Priority = 3;
            ticketP4.Priority = 4;

            var antonio = this.GenerateUser();
            antonio.Name = "António";
            var joao = this.GenerateUser();
            joao.Name = "João";
            var maria = this.GenerateUser();
            maria.Name = "Maria";

            this.repoMock.Setup(r => r.Search(It.Is<TicketFilter>(f => f.CollaboratorId == antonio.Id)))
                .Returns(new List<Ticket>() { ticketP1, ticketP1 });
            this.repoMock.Setup(r => r.Search(It.Is<TicketFilter>(f => f.CollaboratorId == joao.Id)))
                .Returns(new List<Ticket>() { ticketP3 , ticketP3 , ticketP3 , ticketP3 , ticketP4 });
            this.repoMock.Setup(r => r.Search(It.Is<TicketFilter>(f => f.CollaboratorId == maria.Id)))
                .Returns(new List<Ticket>() { ticketP1,ticketP4 });

            //Act
            var result = this.target.GetMostFreeCollaborator(new List<User>() { antonio, joao, maria });

            //Assert
            Assert.AreEqual(maria.Id, result.Id, $"Colaborador mais livre é {result.Name} em vez da Maria");
        }

        [TestMethod()]
        [TestCategory("Unit")]
        public void GetMostFreeCollaborator_Test3_Success()
        {
            //Setup
            var ticketP1 = this.GenerateTicket();
            var ticketP3 = this.GenerateTicket();
            var ticketP4 = this.GenerateTicket();
            ticketP1.Priority = 1;
            ticketP3.Priority = 3;
            ticketP4.Priority = 4;

            var antonio = this.GenerateUser();
            antonio.Name = "António";
            var joao = this.GenerateUser();
            joao.Name = "João";
            var maria = this.GenerateUser();
            maria.Name = "Maria";

            this.repoMock.Setup(r => r.Search(It.Is<TicketFilter>(f=>f.CollaboratorId == antonio.Id)))
                .Returns(new List<Ticket>() { ticketP1, ticketP3 });
            this.repoMock.Setup(r => r.Search(It.Is<TicketFilter>(f => f.CollaboratorId == joao.Id)))
                .Returns(new List<Ticket>() { ticketP1 });
            this.repoMock.Setup(r => r.Search(It.Is<TicketFilter>(f => f.CollaboratorId == maria.Id)))
                .Returns(new List<Ticket>() { ticketP4, ticketP4, ticketP4 });

            //Act
            var result = this.target.GetMostFreeCollaborator(new List<User>() { antonio, joao, maria });

            //Assert
            Assert.AreEqual(maria.Id, result.Id, $"Colaborador mais livre é {result.Name} em vez da Maria");
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
        public User GenerateUser()
        {
            return fixture.Build<User>()
                .With(t => t.Department_Id, ObjectId.GenerateNewId().ToString())
                .Create();
        }
    }
}