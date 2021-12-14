using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManager.Shared.DtoModels;
using TicketManager.Shared.ViewModels;

namespace TicketManager.Client.ClientServices
{
    public interface ITicketsService
    {
        Task CreateTicket(CreateTicketModel ticket);
        Task<List<TicketListItem>> GetTickets();
        Task<TicketDetailsModel> GetTicket(int id);
        List<TicketListItem> Tickets { get; set; }
    }
}
