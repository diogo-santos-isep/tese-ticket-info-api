namespace Presentation.API.Controllers
{
    using BLL.Services.Interfaces;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Models.Domain.Enums;
    using Models.Domain.Models;
    using Models.DTO.Grids;
    using Models.Filters;
    using Presentation.API.Auth;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;

    [Route("api/ticket")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private ITicketService _service;

        public TicketController(ITicketService service)
        {
            this._service = service;
        }

        /// <summary>
        /// Gets All Tickets
        /// | scope: ticket.list
        /// </summary>
        /// <returns>List of tickets</returns>
        [HttpGet]
        [ScopeAndRoleAuthorization(Scopes.TicketListScope)]
        public ActionResult<IEnumerable<Ticket>> GetAll()
        {
            return this._service.Get();
        }

        /// <summary>
        /// Performs a Ticket Search
        /// | scope: ticket.list
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <returns>A Grid containing a list and a count</returns>
        [HttpPost("search")]
        [ScopeAndRoleAuthorization(Scopes.TicketListScope)]
        public ActionResult<TicketGrid> Search(TicketFilter filter)
        {
            filter.CollaboratorId = GetCollaboratorId(HttpContext);
            return this._service.Search(filter);
        }

        private string GetCollaboratorId(HttpContext httpContext)
        {
            var role = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if(role!=null && role.Value == ERole.Collaborator.ToString())
                return httpContext.User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value ?? throw new Exception($"Collaborator id Not Found");
            return null;

        }

        /// <summary>
        /// Performs a Ticket Search for Clients
        /// | scope: ticket.client
        /// </summary>
        /// <param name="clientId">Client identifier</param>
        /// <param name="filter">Filter</param>
        /// <returns>A Grid containing a list and a count</returns>
        [HttpPost("/api/client/{clientId}/ticket/search")]
        [ScopeAndRoleAuthorization(Scopes.TicketClientScope)]
        public ActionResult<TicketClientVMGrid> Search([FromRoute]string clientId,TicketFilter filter)
        {
            filter.ClientId = clientId;
            return this._service.SearchForClient(filter);
        }

        /// <summary>
        /// Gets a Ticket For a Client
        /// | scope: ticket.client
        /// </summary>
        /// <param name="id">Ticket identitifier</param>
        /// <returns>Ticket</returns>
        [HttpGet("/api/client/{clientId}/ticket/{id}")]
        [ScopeAndRoleAuthorization(Scopes.TicketClientScope)]
        public ActionResult<Ticket> GetForClient(string id)
        {
            return this._service.Get(id).ForClient();
        }

        /// <summary>
        /// Gets a ticket 
        /// | scope: ticket
        /// </summary>
        /// <param name="id">Ticket identifier</param>
        /// <returns>Ticket</returns>
        [HttpGet("{id}")]
        [ScopeAndRoleAuthorization(Scopes.TicketScope)]
        public ActionResult<Ticket> Get(string id)
        {
            return this._service.Get(id);
        }

        /// <summary>
        /// Creates a ticket
        /// | scope: ticket.create
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        [HttpPost]
        [ScopeAndRoleAuthorization(Scopes.TicketCreateScope)]
        public ActionResult<Ticket> Create(Ticket ticket)
        {
            return this._service.Create(ticket);
        }

        /// <summary>
        /// Creates a ticket from a message
        /// | scope: ticket.create
        /// </summary>
        /// <param name="message">message</param>
        /// <returns></returns>
        [HttpPost("fromMessage")]
        [ScopeAndRoleAuthorization(Scopes.TicketCreateScope)]
        public ActionResult<Ticket> Create(string message)
        {
            return this._service.Create(message);
        }

        /// <summary>
        /// Updates a ticket
        /// | scope: ticket
        /// </summary>
        /// <param name="id">Ticket identifier</param>
        /// <param name="ticket">Ticket</param>
        /// <returns>Updated Ticket</returns>
        [HttpPut("{id}")]
        [ScopeAndRoleAuthorization(Scopes.TicketScope)]
        public ActionResult<Ticket> Update(string id, Ticket ticket)
        {
            return this._service.Update(id, ticket);
        }

        /// <summary>
        /// Deletes a Ticket
        /// | scope: ticket
        /// | role: admin
        /// </summary>
        /// <param name="id">Ticket identifier</param>
        [HttpDelete("{id}")]
        [ScopeAndRoleAuthorization(Scopes.TicketScope, ERole.Admin)]
        public ActionResult Delete(string id)
        {
            this._service.Delete(id);
            return NoContent();
        }
    }
}
