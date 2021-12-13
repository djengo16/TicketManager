namespace TicketManager.Server.Models
{
    using System;
    using TicketManager.Server.Models.Common;

    public class Comment : IAuditInfo
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int TicketId { get; set; }
        public string ImgUrl { get; set; }
        public virtual Ticket Ticket { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int? ParentId { get; set; }
        public virtual Comment Parent { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}