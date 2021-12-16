using System.ComponentModel.DataAnnotations;

namespace TicketManager.Shared.DtoModels
{
    public class CreateCommentModel
    {
        public int TicketId { get; set; }

        public int ParentId { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
