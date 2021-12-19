namespace TicketManager.Server.Models
{
    using System;
    using System.Collections.Generic;
    using TicketManager.Server.Models.Common;
    using TicketManager.Shared.Models;

    public class Ticket : IAuditInfo
    {
        public Ticket()
        {
            this.Comments = new HashSet<Comment>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Audience Audience { get; set; }
        public string CreatorId { get; set; }
        public virtual ApplicationUser Creator { get; set; }
        public int ReceiverId { get; set; }
        public bool IsOpened { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
