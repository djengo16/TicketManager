using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketManager.Server.Data;
using TicketManager.Server.Models;
using TicketManager.Shared.DtoModels;
using TicketManager.Shared.Models;

namespace TicketManager.Server.Services
{
    public class TicketsService : ITicketsService
    {
        private ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContext;

        public TicketsService(ApplicationDbContext dbContext, IHttpContextAccessor httpContext)
        {
            this._dbContext = dbContext;
            this._httpContext = httpContext;
        }

        public async Task<int> CreateTicketAsync(CreateTicketModel ticketInput)
        {
            var principal = _httpContext.HttpContext.User;
            var loggedInUserId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

            var ticket = await _dbContext.Tickets.AddAsync(new Ticket
            {
                Content = ticketInput.Content,
                Title = ticketInput.Title,
                ReceiverId = ticketInput.ReceiverId,
                CreatorId = loggedInUserId,
                ImgUrl = ticketInput.ImgUrl,
                Audience = (Audience)Enum.Parse(typeof(Audience), ticketInput.Audience)
        });
            await _dbContext.SaveChangesAsync();

            return ticket.Entity.Id;
        }
    }
}