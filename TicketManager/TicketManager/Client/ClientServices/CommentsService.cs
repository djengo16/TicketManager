using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TicketManager.Shared.DtoModels;

namespace TicketManager.Client.ClientServices
{
    public class CommentsService : ICommentsService
    {
        private readonly HttpClient _httpClient;

        public CommentsService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }
        public async Task CreateComment(CreateCommentModel comment)
        {
            var result = await _httpClient.PostAsJsonAsync<CreateCommentModel>("api/comments", comment);
        }
    }
}
