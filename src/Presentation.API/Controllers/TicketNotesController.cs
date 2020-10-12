namespace Presentation.API.Controllers
{
    using BLL.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Models.Domain.Models;
    using Models.DTO.Grids;
    using Models.Filters;
    using Presentation.API.Auth;

    [Route("api/ticket/{ticket_id}/note")]
    [ApiController]
    public class TicketNotesController : ControllerBase
    {
        private ITicketNoteService _service;

        public TicketNotesController(ITicketNoteService service)
        {
            this._service = service;
        }

        /// <summary>
        /// Performs a Search for Ticket Notes
        /// | scope: ticket.notes
        /// </summary>
        /// <param name="ticket_id">ticket containing the notes</param>
        /// <param name="filter">Filter</param>
        /// <returns>A Grid containing a list and a count</returns>
        [HttpPost("search")]
        [ScopeAndRoleAuthorization(Scopes.TicketNotesScope)]
        public ActionResult<TicketNoteGrid> Search(string ticket_id,[FromBody]TicketNoteFilter filter)
        {
            filter.Ticket_Id = ticket_id;
            return this._service.Search(filter);
        }

        /// <summary>
        /// Creates a Ticket Note
        /// | scope: ticket.notes
        /// </summary>
        /// <param name="ticket_id">ticket containing the notes</param>
        /// <param name="ticket">Filter</param>
        /// <returns>Created Ticket Note</returns>
        [HttpPost]
        [ScopeAndRoleAuthorization(Scopes.TicketNotesScope)]
        public ActionResult<TicketNote> Create(string ticket_id, [FromBody] TicketNote ticket)
        {
            ticket.Ticket_Id = ticket_id;
            return this._service.Create(ticket);
        }
    }
}
