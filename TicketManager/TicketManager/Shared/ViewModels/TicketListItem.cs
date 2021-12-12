using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManager.Shared.ViewModels
{
    public class TicketListItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Audience { get; set; }
        public string ImgUrl { get; set; }
        public string CreatorId { get; set; }
        public  string Creator { get; set; }
        public int ReceiverId { get; set; }
    }
}
