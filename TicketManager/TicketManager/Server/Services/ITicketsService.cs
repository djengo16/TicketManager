﻿using System.Threading.Tasks;
using TicketManager.Shared.DtoModels;

namespace TicketManager.Server.Services
{
    public interface ITicketsService
    {
        Task<int> CreateTicketAsync(CreateTicketModel ticketInput);
    }
}
