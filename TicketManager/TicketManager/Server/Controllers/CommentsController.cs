namespace TicketManager.Server.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using TicketManager.Server.Models;
    using TicketManager.Server.Services;
    using TicketManager.Shared.DtoModels;

    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsService _commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            this._commentsService = commentsService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateComment(CreateCommentModel input)
        {
            var parentId =
                input.ParentId == 0 ?
                    (int?)null :
                    input.ParentId;

            if (parentId.HasValue)
            {
                if (!this._commentsService.IsInTicketId(parentId.Value, input.TicketId))
                {
                    return this.BadRequest();
                }
            }

            await this._commentsService.CreateCommentAsync(input.TicketId, input.Content, parentId);
            return this.Ok();
        }
    }
}
