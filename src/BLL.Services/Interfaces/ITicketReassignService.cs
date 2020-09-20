﻿using Models.Domain.Models;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface ITicketReassignService
    {
        Task AssignTicket(Ticket model);
    }
}
