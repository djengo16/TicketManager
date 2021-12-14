namespace TicketManager.Server.Services
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using TicketManager.Server.Data;
    using TicketManager.Server.Models;
    using TicketManager.Shared.DtoModels;
    using TicketManager.Shared.Models;
    using TicketManager.Shared.ViewModels;
    using System.Linq;

    public class TicketsService : ITicketsService
    {
        private ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContext;

        public TicketsService(ApplicationDbContext dbContext,
            IHttpContextAccessor httpContext)
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
                Audience = (Audience)Enum.Parse(typeof(Audience), ticketInput.Audience),
        });
            await _dbContext.SaveChangesAsync();

            return ticket.Entity.Id;
        }

        public async Task<List<TicketListItem>> GetAllTickets()
        {
            var tickets = _dbContext.Tickets
                .Where(x => x.Audience != Audience.Me)
                .Select(x => new TicketListItem
            {
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                Creator = x.Creator.Email,
                ReceiverId = x.ReceiverId,
                CreatedOn = x.CreatedOn.ToString("MM/dd/yyyy h:mm tt"),
            });

            return await tickets.ToListAsync();
        }

        public async Task<List<TicketListItem>> GetPrivateTickets()
        {
            var principal = _httpContext.HttpContext.User;
            var loggedInUserId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            var tickets = _dbContext.Tickets
                .Where(x => x.Audience == Audience.Me && x.CreatorId == loggedInUserId)
                .Select(x => new TicketListItem
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    Creator = x.Creator.Email,
                    ReceiverId = x.ReceiverId,
                });

            return await tickets.ToListAsync();
        }

        public async Task<TicketDetailsModel> GetTicket(int id)
        {
            var result = _dbContext.Tickets.Select(x => new TicketDetailsModel
            {
                Title = x.Title,
                Content = x.Content,
            }).FirstOrDefault(x => x.Id == id);

            return result;
        }
    }
}