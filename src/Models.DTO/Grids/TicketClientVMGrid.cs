namespace Models.DTO.Grids
{
    using Models.DTO.Grid;
    using Models.DTO.ViewModels;
    using System.Linq;

    public class TicketClientVMGrid : Grid<TicketClientListVM>
    {
        public TicketClientVMGrid(TicketGrid ticketGrid)
        {
            this.Count = ticketGrid.Count;
            this.List = ticketGrid.List.Select(i => new TicketClientListVM(i));
        }
    }
}
