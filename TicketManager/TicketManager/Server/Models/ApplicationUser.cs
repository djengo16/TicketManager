using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace TicketManager.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}