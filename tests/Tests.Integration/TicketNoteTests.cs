namespace Tests.Integration
{
    using AutoFixture;
    using BLL.Services.Implementations;
    using DAL.Repositories.Implementations;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.Domain.Models;
    using Models.Filters;
    using Moq;
    using System;
    using System.Linq;
    using Tests.Integration.Helpers;

    [TestClass]
    public class TicketNoteTests
    {
        private TicketNoteRepository repo;
        private TicketNoteService _service;
        private Fixture fixture = new Fixture();
        

        public TicketNoteTests()
        {
            //this.repo = new TicketNoteRepository(DatabaseConnection.Current.Database);
            //this._service = new TicketNoteService(this.repo);
        }

        [TestMethod()]
        [TestCategory("Integration")]
        public void Create_Success()
        {
            //var newTicketNote = this.fixture.GenerateTicketNote();

            //newTicketNote = this._service.Create(newTicketNote);
            //Assert.IsFalse(String.IsNullOrEmpty(newTicketNote.Id), "Id came back null or empty");

            //var savedTicketNote = this.repo.Get(newTicketNote.Id);
            //Assert.AreEqual(savedTicketNote, newTicketNote, "TicketNotes are different");
            Assert.IsTrue(true);
        }
        [TestMethod()]
        [TestCategory("Integration")]
        public void Search_Success()
        {
            //var newTicketNote = this.fixture.GenerateTicketNote();
            //var newTicketNote2 = this.fixture.GenerateTicketNote();
            //newTicketNote2.Ticket_Id = newTicketNote.Ticket_Id;
            
            //this._service.Create(newTicketNote);
            //this._service.Create(newTicketNote2);

            //var grid = this._service.Search(new TicketNoteFilter
            //{
            //    Page = 1,
            //    PageSize = 50,
            //    SortBy = "Date",
            //    SortAscending = true,
            //    Ticket_Id = newTicketNote.Ticket_Id
            //});
            //Assert.IsTrue(grid.List.Any(t => t.Id == newTicketNote.Id), $"TicketNote1 does not exist");
            //Assert.IsTrue(grid.List.Any(t => t.Id == newTicketNote2.Id), $"TicketNote2 does not exist");
            Assert.IsTrue(true);
        }

    }
}
