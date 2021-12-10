using TicketManager.Shared;
using TicketManager.Shared.Models;

namespace TicketManager.Server.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Audience Audience { get; set; }
        public string ImgUrl { get; set; }
        public string CreatorId { get; set; }
        public virtual ApplicationUser Creator { get; set; }
        public int ReceiverId { get; set; }
    }
}
