namespace BLL.Services.Interfaces
{
    using Models.Domain.Models;
    using Models.DTO.Grids;
    using Models.Filters;

    public interface ITicketNoteService
    {
        TicketNote Create(TicketNote model);
        TicketNoteGrid Search(TicketNoteFilter filter);
    }
}
