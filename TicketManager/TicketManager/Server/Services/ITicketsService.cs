namespace TicketManager.Server.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TicketManager.Shared.DtoModels;
    using TicketManager.Shared.ViewModels;

    public interface ITicketsService
    {
        Task<int> CreateTicketAsync(CreateTicketModel ticketInput);
        Task UpdateTicketAsync(CreateTicketModel ticketUpdate, int id);
        Task<List<TicketListItem>> GetAllTickets();
        Task<List<TicketListItem>> GetTechnicalSupportTickets(string userId);
        Task<List<TicketListItem>> GetOfficeSupportTickets(string userId);
        Task<List<TicketListItem>> GetPrivateTickets();
        Task<TicketDetailsModel> GetTicket(int id);
        Task<CreateTicketModel> GetTicketToUpdate(int id);
        Task DeleteTicket(int id);
    }
}
