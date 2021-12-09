using System.Threading.Tasks;
using TicketManager.Server.Data;
using TicketManager.Server.Models;
using TicketManager.Server.Models.DataModels;

namespace TicketManager.Server.Services
{
    public class TicketsService : ITicketsService
    {
        private ApplicationDbContext _dbContext;
        public TicketsService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateTicketAsync(TicketInputModel ticketInput)
        {
            var ticket = await _dbContext.Tickets.AddAsync(new Ticket
            {
                Content = ticketInput.Content,
                Title = ticketInput.Title,
                ReceiverId = ticketInput.ReceiverId,
                CreatorId = ticketInput.CreatorId,
                ImgUrl = ticketInput.ImgUrl,
                Audience = ticketInput.Audience
            });
            await _dbContext.SaveChangesAsync();

            return ticket.Entity.Id;
        }
    }
}
