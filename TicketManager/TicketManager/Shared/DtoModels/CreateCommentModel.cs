namespace TicketManager.Shared.DtoModels
{
    public class CreateCommentModel
    {
        public int TicketId { get; set; }

        public int ParentId { get; set; }

        public string Content { get; set; }
    }
}
