namespace BLL.Services.Implementations.Tests
{
    using AutoFixture;
    using DAL.Repositories.Interfaces;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.Domain.Models;
    using Models.Filters;
    using Moq;

    [TestClass()]
    public class TicketNoteServiceTests
    {
        private Mock<ITicketNoteRepository> repoMock;
        private TicketNoteService target;
        private Fixture fixture = new Fixture();

        public TicketNoteServiceTests()
        {
            this.repoMock = new Mock<ITicketNoteRepository>();
            this.target = new TicketNoteService(repoMock.Object);
        }

        [TestMethod()]
        public void Create_Success()
        {
            var model = this.fixture.Create<TicketNote>();
            this.target.Create(model);

            this.repoMock.Verify(x => x.Create(model), Times.Once, "Create não foi chamado");
        }

        [TestMethod()]
        public void Search_Success()
        {
            var filter = this.fixture.Create<TicketNoteFilter>();
            this.target.Search(filter);

            this.repoMock.Verify(x => x.Search(filter), Times.Once, "Search não foi chamado");
            this.repoMock.Verify(x => x.Count(filter), Times.Once, "Count não foi chamado");
        }
    }
}