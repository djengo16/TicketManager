using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TicketManager.Server.Models;
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTicket(CreateTicketModel ticket)
        {
            var res = await this._ticketsService.CreateTicketAsync(ticket);
            return this.Ok(res);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetTickets()
        {
            var tickets = await _ticketsService.GetAllTickets();
            return base.Ok(tickets);
        }
    }
}
