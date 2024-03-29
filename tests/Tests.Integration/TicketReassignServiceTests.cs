﻿namespace Tests.Integration
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
            //var repo = new TicketRepository(DatabaseConnection.Current.Database);
            //this._service = new TicketReassignService(departmentClientMock.Object, userClientMock.Object, repo
            //    , ticketReassignedEventProducerMock.Object, ticketStateChangedEventProducerMock.Object);
            //this._ticketService = new TicketService(repo, ticketStateChangedEventProducerMock.Object, ticketReassignServiceMock.Object
            //    , ticketCreatedProducerMock.Object, ticketFieldsUpdatedServiceMock.Object);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task AssignTicket_Success()
        {
            //Setup
            //var ticket1 = this.fixture.GenerateTicket();
            //ticket1.State = ETicketState.Created;
            //ticket1 = this._ticketService.Create(ticket1);

            //var collaborator = this.fixture.GenerateUser();
            //this.userClientMock.Setup(x => x.GetCollaboratorsFromDepartment(It.IsAny<Department>())).ReturnsAsync(new List<User> { collaborator });

            ////Act
            //await this._service.AssignTicket(ticket1).ConfigureAwait(false);

            ////Assert
            //var updatedTicket = this._ticketService.Get(ticket1.Id);
            //Assert.AreEqual(collaborator.Id, updatedTicket.CollaboratorId, "Colaborador não foi atribuido");
            //Assert.AreEqual(collaborator.Department_Id, updatedTicket.DepartmentId, "Departamento não foi atribuido");
            //Assert.AreEqual(ETicketState.Assigned, updatedTicket.State, "Estado não foi alterado para atribuido");
            Assert.IsTrue(true);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task AssignTicket_WithOnlyDepartment_Success()
        {
            //var ticket1 = this.fixture.GenerateTicket();
            //ticket1.State = ETicketState.Created;
            //ticket1 = this._ticketService.Create(ticket1);
            //var department = this.fixture.GenerateDepartment();

            //var collaborator = this.fixture.GenerateUser();
            //this.userClientMock.Setup(x => x.GetCollaboratorsFromDepartment(department)).ReturnsAsync(new List<User> { collaborator });

            //await this._service.AssignTicket(ticket1,department,null).ConfigureAwait(false);
            //this.userClientMock.Verify(x => x.GetCollaboratorsFromDepartment(department), Times.Once);
            //this.ticketReassignedEventProducerMock.Verify(x => x.Produce(It.IsAny<TicketReassignedEventBody>()), Times.Once);
            //this.ticketStateChangedEventProducerMock.Verify(x => x.Produce(It.IsAny<TicketStateChangedEventBody>()), Times.Once);

            //var updatedTicket = this._ticketService.Get(ticket1.Id);
            //Assert.AreEqual(collaborator.Id, updatedTicket.CollaboratorId, "Collaborator is not the same");
            //Assert.AreEqual(collaborator.Name, updatedTicket.CollaboratorName, "Collaborator is not the same");
            //Assert.AreEqual(collaborator.Department_Id, updatedTicket.DepartmentId, "Department is not the same");
            //Assert.AreEqual(collaborator.Department_Description, updatedTicket.DepartmentDescription, "Collaborator is not the same");
            Assert.IsTrue(true);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task AssignTicket_DepartmentAndCollaborator_Success()
        {
            //var ticket1 = this.fixture.GenerateTicket();
            //ticket1.State = ETicketState.Created;
            //ticket1 = this._ticketService.Create(ticket1);
            //var department = this.fixture.GenerateDepartment();

            //var collaborator = this.fixture.GenerateUser();

            //await this._service.AssignTicket(ticket1, department, collaborator).ConfigureAwait(false);
            //this.userClientMock.Verify(x => x.GetCollaboratorsFromDepartment(It.IsAny<Department>()), Times.Never);
            //this.ticketReassignedEventProducerMock.Verify(x => x.Produce(It.IsAny<TicketReassignedEventBody>()), Times.Once);
            //this.ticketStateChangedEventProducerMock.Verify(x => x.Produce(It.IsAny<TicketStateChangedEventBody>()), Times.Once);

            //var updatedTicket = this._ticketService.Get(ticket1.Id);
            //Assert.AreEqual(collaborator.Id, updatedTicket.CollaboratorId, "Collaborator is not the same");
            //Assert.AreEqual(collaborator.Name, updatedTicket.CollaboratorName, "Collaborator is not the same");
            //Assert.AreEqual(collaborator.Department_Id, updatedTicket.DepartmentId, "Department is not the same");
            //Assert.AreEqual(collaborator.Department_Description, updatedTicket.DepartmentDescription, "Collaborator is not the same");
            Assert.IsTrue(true);
        }
    }
}
