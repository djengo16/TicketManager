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
        private readonly UserManager<ApplicationUser> _userManager;

        public TicketsController(ITicketsService ticketsService,
             UserManager<ApplicationUser> userManager)
        {
            this._ticketsService = ticketsService;
            this._userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTicket(CreateTicketModel ticket)
        {
            var res = await this._ticketsService.CreateTicketAsync(ticket);
            return this.Ok(res);
        }
    }
}
