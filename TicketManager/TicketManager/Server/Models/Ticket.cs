namespace TicketManager.Server.Models
{
    using System;
    using TicketManager.Server.Models.Common;
    using TicketManager.Shared.Models;

    public class Ticket : IAuditInfo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Audience Audience { get; set; }
        public string ImgUrl { get; set; }
        public string CreatorId { get; set; }
        public virtual ApplicationUser Creator { get; set; }
        public int ReceiverId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
