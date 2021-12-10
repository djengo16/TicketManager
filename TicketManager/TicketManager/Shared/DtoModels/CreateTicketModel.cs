using System.ComponentModel.DataAnnotations;
using TicketManager.Shared.Models;

namespace TicketManager.Shared.DtoModels
{
    public class CreateTicketModel
    {
        [Required]
        [MaxLength(30)]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public Audience Audience { get; set; }
        [Required]
        public string ImgUrl { get; set; }
        [Required]
        public string CreatorId { get; set; }
        [Required]
        public int ReceiverId { get; set; }
    }
}
