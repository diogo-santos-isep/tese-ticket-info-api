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
    using System.Security.Principal;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private ITicketService _service;

        public TicketController(ITicketService service)
        {
            this._service = service;
        }

        [HttpGet]
        [ScopeAndRoleAuthorization(Scopes.TicketListScope)]
        public ActionResult<IEnumerable<Ticket>> GetAll()
        {
            return this._service.Get();
        }

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

        [HttpPost("/api/client/{clientId}/ticket/search")]
        [ScopeAndRoleAuthorization(Scopes.TicketClientScope)]
        public ActionResult<TicketClientVMGrid> Search([FromRoute]string clientId,TicketFilter filter)
        {
            filter.ClientId = clientId;
            return this._service.SearchForClient(filter);
        }

        [HttpGet("/api/client/{clientId}/ticket/{id}")]
        [ScopeAndRoleAuthorization(Scopes.TicketClientScope)]
        public ActionResult<Ticket> GetForClient(string id)
        {
            return this._service.Get(id).ForClient();
        }

        [HttpGet("{id}")]
        [ScopeAndRoleAuthorization(Scopes.TicketScope)]
        public ActionResult<Ticket> Get(string id)
        {
            return this._service.Get(id);
        }

        [HttpPost]
        [ScopeAndRoleAuthorization(Scopes.TicketCreateScope)]
        public ActionResult<Ticket> Create(Ticket ticket)
        {
            return this._service.Create(ticket);
        }

        [HttpPost("fromMessage")]
        [ScopeAndRoleAuthorization(Scopes.TicketCreateScope)]
        public ActionResult<Ticket> Create(string message)
        {
            return this._service.Create(message);
        }

        [HttpPut("{id}")]
        [ScopeAndRoleAuthorization(Scopes.TicketScope)]
        public ActionResult<Ticket> Update(string id, Ticket ticket)
        {
            return this._service.Update(id, ticket);
        }

        [HttpDelete("{id}")]
        [ScopeAndRoleAuthorization(Scopes.TicketScope, ERole.Admin)]
        public ActionResult<Ticket> Delete(string id)
        {
            this._service.Delete(id);
            return NoContent();
        }
    }
}
