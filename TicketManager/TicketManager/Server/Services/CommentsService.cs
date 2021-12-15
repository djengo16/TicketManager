namespace TicketManager.Server.Services
{
    using System.Threading.Tasks;
    using TicketManager.Server.Data;
    using TicketManager.Server.Models;

    public class CommentsService : ICommentsService
    {
        private readonly ApplicationDbContext _dbContext;

        public CommentsService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task CreateCommentAsync(int ticketId, string userId, string content, int? parentId = null)
        {
            var comment = new Comment
            {
                Content = content,
                ParentId = parentId,
                TicketId = ticketId,
                UserId = userId,
            };

            await _dbContext.Comments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();
        }
    }
}