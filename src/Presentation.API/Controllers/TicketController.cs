namespace Presentation.API.Controllers
{
    using BLL.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Models.Domain.Models;
    using System.Collections.Generic;
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
        public ActionResult<IEnumerable<Ticket>> GetAll()
        {
            return this._service.Get();
        }

        [HttpGet("{id}")]
        public ActionResult<Ticket> Get(string id)
        {
            return this._service.Get(id);
        }

        [HttpPost]
        public ActionResult<Ticket> Create(Ticket ticket)
        {
            return this._service.Create(ticket);
        }

        [HttpPut("{id}")]
        public ActionResult<Ticket> Update(string id, Ticket ticket)
        {
            return this._service.Update(id, ticket);
        }

        [HttpDelete("{id}")]
        public ActionResult<Ticket> Delete(string id)
        {
            this._service.Delete(id);
            return NoContent();
        }
    }
}
