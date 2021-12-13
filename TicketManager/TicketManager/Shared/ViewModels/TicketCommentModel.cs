namespace TicketManager.Shared.ViewModels
{
    using System;
    public class TicketCommentModel
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
        public string ImgUrl { get; set; }
    }
}
