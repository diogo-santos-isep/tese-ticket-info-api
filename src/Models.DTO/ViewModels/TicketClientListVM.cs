using Models.Domain.Enums;
using Models.Domain.Models;

namespace Models.DTO.ViewModels
{
    public class TicketClientListVM
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public ETicketState State { get; set; }
        public string CollaboratorName { get; set; }
        public string CategoryDescription { get; set; }
        public string CategoryColor { get; set; }

        public TicketClientListVM(Ticket ticket)
        {
            this.Id = ticket.Id;
            this.Code = ticket.Code;
            this.Subject = ticket.Subject;
            this.Description = ticket.Description;
            this.State = ticket.State;
            this.CollaboratorName = ticket.CollaboratorName;
            this.CategoryDescription = ticket.CategoryDescription;
            this.CategoryColor = ticket.CategoryColor;
        }
    }
}
