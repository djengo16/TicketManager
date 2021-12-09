namespace TicketManager.Server.Models.DataModels
{
    public class TicketInputModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Audience Audience { get; set; }
        public string ImgUrl { get; set; }
        public string CreatorId { get; set; }
        public int ReceiverId { get; set; }
    }
}