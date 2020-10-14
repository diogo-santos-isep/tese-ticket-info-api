using Models.Domain.Models;
using MongoDB.Driver;

namespace Models.DTO.DTOs
{
    public class TicketReassignDTO
    {
        public Department Department { get; set; }
        public User Collaborator { get; set; }
    }
}
