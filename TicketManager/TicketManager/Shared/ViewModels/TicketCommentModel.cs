namespace TicketManager.Shared.ViewModels
{
    using Ganss.XSS;
    using System;
    public class TicketCommentModel
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Content { get; set; }
        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);
        public string UserEmail { get; set; }
    }
}
