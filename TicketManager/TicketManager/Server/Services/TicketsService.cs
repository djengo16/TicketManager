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
        private readonly IRolesService _rolesService;

        public TicketsService(ApplicationDbContext dbContext,
            IHttpContextAccessor httpContext,
            IRolesService rolesService)
        {
            this._dbContext = dbContext;
            this._httpContext = httpContext;
            this._rolesService = rolesService;
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
            var test = _dbContext.Tickets;

            var result =  await _dbContext.Tickets
                .Where(x => x.Id == id)
                .Select(x => new TicketDetailsModel
            {
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                CreatorEmail = x.Creator.Email,
                CreatedOn = x.CreatedOn,
                CreatorId = x.CreatorId,
                ReceiverRole = _rolesService.GetRoleById(x.ReceiverId),
                //Comments = x.Comments.Select(y => new TicketCommentModel
                //{
                //    CreatedOn = y.CreatedOn,
                //    Content = y.Content,
                //    UserEmail = y.User.Email,
                //}).ToList()
            }).FirstOrDefaultAsync();

            return  result;
        }

        public async Task DeleteTicket(int id)
        {
            var ticketToRemove = await this.GetTicketFromDb(id);

           _dbContext.Remove(ticketToRemove);
           await _dbContext.SaveChangesAsync();
        }

        public async Task<CreateTicketModel> GetTicketToUpdate(int id)
        {
            var ticket =  await _dbContext.Tickets.Where(x => x.Id == id)
                .Select(x => new CreateTicketModel
                {
                    CreatorId = x.CreatorId,
                    Content = x.Content,
                    Title = x.Title,
                    Audience = x.Audience.ToString(),
                })
                .FirstOrDefaultAsync();

            return ticket;
        }

        public async Task UpdateTicketAsync(CreateTicketModel updatedTicket, int id)
        {
            var ticketToUpdate =  await this.GetTicketFromDb(id);

            ticketToUpdate.Title = updatedTicket.Title;
            ticketToUpdate.Content = updatedTicket.Content;
            ticketToUpdate.Audience = (Audience)Enum.Parse(typeof(Audience), updatedTicket.Audience);
            ticketToUpdate.ReceiverId = updatedTicket.ReceiverId;

            _dbContext.Tickets.Update(ticketToUpdate);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<Ticket> GetTicketFromDb(int id)
        {
            return await _dbContext.Tickets
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}