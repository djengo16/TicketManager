using System.Threading.Tasks;
using TicketManager.Server.Models.DataModels;

namespace TicketManager.Server.Services
{
    public interface ITicketsService
    {
        Task<int> CreateTicketAsync(TicketInputModel ticketInput);
    }
}
