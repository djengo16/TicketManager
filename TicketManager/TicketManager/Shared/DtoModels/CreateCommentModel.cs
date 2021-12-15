namespace TicketManager.Shared.DtoModels
{
    public class CreateCommentModel
    {
        public int PostId { get; set; }

        public int ParentId { get; set; }

        public string Content { get; set; }
    }
}
