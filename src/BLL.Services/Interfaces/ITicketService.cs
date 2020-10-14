namespace BLL.Services.Interfaces
{
    using Models.Domain.Models;
    using Models.DTO.Grids;
    using Models.Filters;

    public interface ITicketService : IService<Ticket>
    {
        TicketClientVMGrid SearchForClient(TicketFilter filter);
        TicketGrid Search(TicketFilter filter);
        Ticket Create(string message);
    }
}
