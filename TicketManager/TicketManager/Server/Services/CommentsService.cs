namespace TicketManager.Server.Services
{
    using Microsoft.AspNetCore.Http;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using TicketManager.Server.Data;
    using TicketManager.Server.Models;

    public class CommentsService : ICommentsService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContext;

        public CommentsService(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor)
        {
            this._dbContext = dbContext;
            this._httpContext = contextAccessor;
        }
        public async Task CreateCommentAsync(int ticketId, string content, int? parentId = null)
        {
            var principal = _httpContext.HttpContext.User;
            var loggedInUserId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

            var comment = new Comment
            {
                Content = content,
                ParentId = parentId,
                TicketId = ticketId,
                UserId = loggedInUserId,
            };

            await _dbContext.Comments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();
        }

        public bool IsInTicketId(int commentId, int ticketId)
        {
            var commentTicketId = this._dbContext.Comments.Where(x => x.Id == commentId)
                .Select(x => x.TicketId).FirstOrDefault();
            return commentTicketId == ticketId;
        }
    }
}