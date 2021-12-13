using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TicketManager.Shared.ViewModels
{
    public class TicketListItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ShortContent
        {
            get
            {
                var content = WebUtility.HtmlDecode(Regex.Replace(this.Content, @"<[^>]+>", string.Empty));
                return content.Length > 60
                        ? content.Substring(0, 60) + "..."
                        : content;
            }
        }
        public string Audience { get; set; }
        public string CreatorId { get; set; }
        public  string Creator { get; set; }
        public int ReceiverId { get; set; }
        public string CreatedOn { get; set; }
    }
}
