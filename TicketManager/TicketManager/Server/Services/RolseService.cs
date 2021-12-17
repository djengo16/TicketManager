using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManager.Server.Data;

namespace TicketManager.Server.Services
{
    public class RolseService : IRolesService
    {
        ApplicationDbContext _dbContext;
        public RolseService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task CreateUserRole(string roleId, string userId)
        {
          //  await _dbContext.UserRoles.AddAsync(new IdentityUserRole<string>( UserId = userId, RoleId = roleId );
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

        public string GetRoleById(int roleId)
        {
            string roleIdAsString = roleId.ToString();
            var role = _dbContext.Roles.Where(x => x.Id == roleIdAsString).FirstOrDefault();

            return role.Name;
        }

        public string GetRoleIdByName(string name)
        {
            return _dbContext.Roles.Where(x => x.Name.ToUpper() == name)
                .FirstOrDefault().Id;
        }
    }
}
