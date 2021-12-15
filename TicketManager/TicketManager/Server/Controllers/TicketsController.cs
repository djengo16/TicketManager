using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSignleTicket(int id)
        {
            var ticket =  await this._ticketsService.GetTicket(id);

            return Ok(ticket);
        }

        [HttpGet("toupdate/{id}")]
        public async Task<IActionResult> GetTicketToUpdate(int id)
        {
            var ticket = await this._ticketsService.GetTicketToUpdate(id);

            return Ok(ticket);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, CreateTicketModel updatedTicket)
        {
            await _ticketsService.UpdateTicketAsync(updatedTicket, id);

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            await this._ticketsService.DeleteTicket(id);
            return Ok();
        }
    }
}
