using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketManager.Server.Services
{
    public interface IRolesService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();

         Task CreateUserRole(string roleId, string userId);

        string GetRoleById(int role);

        string GetRoleIdByName(string name);
    }
}
