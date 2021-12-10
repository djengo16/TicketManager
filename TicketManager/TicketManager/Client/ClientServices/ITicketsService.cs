using System.Threading.Tasks;
using TicketManager.Shared.DtoModels;

namespace TicketManager.Client.ClientServices
{
    public interface ITicketsService
    {
        Task<CreateTicketModel> CreateTicket(CreateTicketModel ticket);
    }
}
