using System.Threading.Tasks;

namespace TicketManager.Server.Services
{
    public interface ICommentsService
    {
        Task CreateCommentAsync(int ticketId, string content, int? parentId = null);

        bool IsInTicketId(int commentId, int ticketId);
    }
}
