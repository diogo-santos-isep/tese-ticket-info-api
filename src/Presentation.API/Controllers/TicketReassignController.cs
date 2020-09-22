namespace Presentation.API.Controllers
{
    using BLL.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Models.DTO.DTOs;
    using Presentation.API.Auth;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class TicketReassignController : ControllerBase
    {
        private ITicketReassignService _service;
        private ITicketService _ticketService;

        public TicketReassignController(ITicketReassignService service, ITicketService ticketService)
        {
            this._service = service;
            this._ticketService = ticketService;
        }

        [HttpPost("/api/ticket/{ticket_id}/reassign")]
        [ScopeAndRoleAuthorization(Scopes.TicketScope)]
        public async Task<ActionResult> Create([FromRoute] string ticket_id, TicketReassignDTO ticketDTO)
        {
            var ticket = this._ticketService.Get(ticket_id);
            await this._service.AssignTicket(ticket, ticketDTO.Department, ticketDTO.Collaborator).ConfigureAwait(false);
            return Ok();
        }

    }
}
