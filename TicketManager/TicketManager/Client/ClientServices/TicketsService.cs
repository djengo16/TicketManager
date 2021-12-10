using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TicketManager.Shared.DtoModels;

namespace TicketManager.Client.ClientServices
{
    public class TicketsService : ITicketsService
    {
        private readonly HttpClient _httpClient;

        public TicketsService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }
        public async Task<CreateTicketModel> CreateTicket(CreateTicketModel ticket)
        {
           var result = await  _httpClient.PostAsJsonAsync<CreateTicketModel>("api/tickets", ticket);
            var responseObj = await result.Content.ReadFromJsonAsync<CreateTicketModel>();

            return responseObj;
        }
    }
}
