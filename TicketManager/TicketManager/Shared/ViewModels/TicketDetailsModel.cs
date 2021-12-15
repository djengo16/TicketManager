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
        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);
        public string CreatorEmail { get; set; }
        public string CreatorRole { get; set; }
        public string CreatorId { get; set; }
        public string ReceiverRole { get; set; }
        public IEnumerable<TicketCommentModel> Comments { get; set; }
    }
}