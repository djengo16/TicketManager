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
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentsController(ICommentsService commentsService,
            UserManager<ApplicationUser> userManager)
        {
            this._commentsService = commentsService;
            this._userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateComment(CreateCommentModel input)
        {
            var parentId =
                input.ParentId == 0 ?
                    (int?)null :
                    input.ParentId;

            //if (parentId.HasValue)
            //{
            //    if (!this.commentsService.IsInPostId(parentId.Value, input.PostId))
            //    {
            //        return this.BadRequest();
            //    }
            //}

            var userId = this._userManager.GetUserId(this.User);
            await this._commentsService.CreateCommentAsync(input.PostId, userId, input.Content, parentId);
            return this.RedirectToAction("ById", "Posts", new { id = input.PostId });
        }
    }
}
