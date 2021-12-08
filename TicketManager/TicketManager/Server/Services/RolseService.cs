using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManager.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace TicketManager.Server.Services
{
    public class RolseService : IRolesService
    {
        ApplicationDbContext _dbContext;
        public RolseService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this._dbContext.Roles
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                })
                .OrderBy(x => x.Name)
                .ToList().Select(x => new KeyValuePair<string, string>(x.Id, x.Name));
        }
    }
}
