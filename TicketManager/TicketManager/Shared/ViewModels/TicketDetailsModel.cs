namespace TicketManager.Shared.ViewModels
{
    using Ganss.XSS;
    using System;
    using System.Collections.Generic;

    public class TicketDetailsModel
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string SanitizedConten => new HtmlSanitizer().Sanitize(this.Content);
        public string ImgUrl { get; set; }
        public string CreatorName { get; set; }
        public string CreatorRole { get; set; }
        public IEnumerable<TicketCommentModel> Comments { get; set; }
    }
}