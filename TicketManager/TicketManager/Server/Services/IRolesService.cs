using System.Collections.Generic;

namespace TicketManager.Server.Services
{
    public interface IRolesService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
    }
}
