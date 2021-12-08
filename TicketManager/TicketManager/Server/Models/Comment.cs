namespace TicketManager.Server.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int? ParentId { get; set; }
        public virtual Comment Parent { get; set; }
    }
}
