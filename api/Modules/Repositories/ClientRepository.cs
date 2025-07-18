using Api.Modules.Clients;
using System.Reflection;

namespace Api.Modules.Repositories
{
    public class ClientRepository(List<Client> clients)
    {
        private readonly List<Client> _activeClients = [..clients.Where(client => client.Deleted == false)];
        public void Create(ClientDto client)
        {
            client.Id = Guid.NewGuid();
            client.Date = DateTime.Now;
            clients.Add(DtoMapper.ToEntity(client));
        }
        public void Update(ClientDto client)
        {
            int clientIndex = clients.FindIndex(r => r.Id == client.Id);
            clients[clientIndex] = DtoMapper.ToEntity(client);
        }
        public void Delete(Guid id)
        {
            Client client = clients.First(c => c.Id == id);
            client.Deleted = true;
        }
        public ClientDto GetOne(Guid id)
        {
            return DtoMapper.ToDto(_activeClients.First(c => c.Id == id));
        }
        public List<Client> GetAll()
        {
            return [.. _activeClients];
        }
        public List<Client> Search(string query)
        {
            return [.. _activeClients
                .Where(client => $"{client.Name} {client.Email.Split("@")[0]}"
                .Contains(query, StringComparison.CurrentCultureIgnoreCase))];
        }
        public List<Client> Sort(string sortKey, bool descending)
        {
            PropertyInfo? sortProperty = sortKey == "default" ? typeof(Client).GetProperty("Id") : typeof(Client).GetProperty(sortKey, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            IOrderedEnumerable<Client> sortedClients = descending ? _activeClients.OrderByDescending(sortProperty.GetValue) : _activeClients.OrderBy(sortProperty.GetValue);

            return [.. sortedClients];
        }
    }
}
