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
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using TicketManager.Shared;
    using Microsoft.AspNetCore.Identity;

    public class TicketsService : ITicketsService
    {
        private ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IRolesService _rolesService;
        private readonly IConfigurationProvider mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public TicketsService(ApplicationDbContext dbContext,
            IHttpContextAccessor httpContext,
            IRolesService rolesService,
            IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            this._dbContext = dbContext;
            this._httpContext = httpContext;
            this._rolesService = rolesService;
            this.mapper = mapper.ConfigurationProvider;
            this._userManager = userManager;
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
                IsOpened = false,
            });
            await _dbContext.SaveChangesAsync();

            return ticket.Entity.Id;
        }

        public async Task<List<TicketListItem>> GetAllTickets()
        {

            var principal = _httpContext.HttpContext.User;
            var loggedInUserId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

            var aspuser = await this._userManager.FindByIdAsync(loggedInUserId);

            if (await _userManager.IsInRoleAsync(aspuser, Constants.TechnicalSupport))
            {
                return await GetTechnicalSupportTickets(loggedInUserId);
            }
            else if (await _userManager.IsInRoleAsync(aspuser, Constants.OfficeSupport))
            {
                return await GetOfficeSupportTickets(loggedInUserId);
            }

            //othervise return all of them
            var tickets = _dbContext.Tickets
                .Where(x => x.Audience != Audience.Me ||
                  (x.Audience == Audience.Me && loggedInUserId == x.CreatorId))
                .OrderByDescending(x => x.CreatedOn)
                .ProjectTo<TicketListItem>(this.mapper);

            return await tickets.ToListAsync();
        }

        public async Task<List<TicketListItem>> GetPrivateTickets()
        {
            var principal = _httpContext.HttpContext.User;
            var loggedInUserId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            var tickets = _dbContext.Tickets
                .Where(x => x.Audience == Audience.Me && x.CreatorId == loggedInUserId)
                .OrderByDescending(x => x.CreatedOn)
                .ProjectTo<TicketListItem>(this.mapper);

            return await tickets.ToListAsync();
        }

        public async Task<TicketDetailsModel> GetTicket(int id)
        {
            var result = await _dbContext.Tickets
                .Where(x => x.Id == id)
                .ProjectTo<TicketDetailsModel>(this.mapper)
                .FirstOrDefaultAsync();

            if (!result.IsOpened)
            {
                await UpdateTicketStatus(id);
            }

            return result;
        }

        private async Task UpdateTicketStatus(int ticketId)
        {
            var ticket = await _dbContext.Tickets
                .Where(x => x.Id == ticketId)
                .FirstOrDefaultAsync();

            ticket.IsOpened = true;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTicket(int id)
        {
            var ticketToRemove = await this.GetTicketFromDb(id);

            _dbContext.Remove(ticketToRemove);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<CreateTicketModel> GetTicketToUpdate(int id)
        {
            var ticket = await _dbContext.Tickets.Where(x => x.Id == id)
                .Select(x => new CreateTicketModel
                {
                    CreatorId = x.CreatorId,
                    Content = x.Content,
                    Title = x.Title,
                    Audience = x.Audience.ToString(),
                    ReceiverId = x.ReceiverId,
                })
                .FirstOrDefaultAsync();

            return ticket;
        }

        public async Task UpdateTicketAsync(CreateTicketModel updatedTicket, int id)
        {
            var ticketToUpdate = await this.GetTicketFromDb(id);

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

        public async Task<List<TicketListItem>> GetTechnicalSupportTickets(string userId)
        {
            var techSupportRoleId = int.Parse(_rolesService.GetRoleIdByName(Constants.TechnicalSupport));
            var result =  _dbContext.Tickets
                .Where(x => (x.ReceiverId == techSupportRoleId && x.Audience != Audience.Me) || 
                (x.Audience == Audience.Me && x.CreatorId == userId))
                .OrderByDescending(x => x.CreatedOn)
                .ProjectTo<TicketListItem>(this.mapper);

            return await result.ToListAsync();
        }

        public async Task<List<TicketListItem>> GetOfficeSupportTickets(string userId)
        {
            var getOfficeSupportRoleId = int.Parse(_rolesService.GetRoleIdByName(Constants.OfficeSupport));

            var result = _dbContext.Tickets
                .Where(x => (x.ReceiverId == getOfficeSupportRoleId && x.Audience != Audience.Me) ||
                (x.Audience == Audience.Me && x.CreatorId == userId))
                .OrderByDescending(x => x.CreatedOn)
                .ProjectTo<TicketListItem>(this.mapper);

            return await result.ToListAsync();
        }
    }
}