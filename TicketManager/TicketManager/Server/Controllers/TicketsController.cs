using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManager.Server.Services;
using TicketManager.Shared.DtoModels;

namespace TicketManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketsService _ticketsService;

        public TicketsController(ITicketsService ticketsService)
        {
            this._ticketsService = ticketsService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateTicket(CreateTicketModel ticket)
        {
            await this._ticketsService.CreateTicketAsync(ticket);
            return this.Ok();
        }
    }
}
