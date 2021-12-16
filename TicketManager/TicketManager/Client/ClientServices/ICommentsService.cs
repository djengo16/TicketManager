using System.Threading.Tasks;
using TicketManager.Shared.DtoModels;
namespace TicketManager.Client.ClientServices
{
    public interface ICommentsService
    {
        Task CreateComment(CreateCommentModel comment);
    }
}
