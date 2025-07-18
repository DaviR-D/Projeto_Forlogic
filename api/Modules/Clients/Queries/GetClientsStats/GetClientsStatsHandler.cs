using Api.Modules.Repositories;

namespace Api.Modules.Clients.Queries.GetClientsStats
{
    public class GetClientsStatsHandler(ClientRepository repository)
    {
        public ClientsStatsDto Handle()
        {
            List<Client> activeClients = repository.GetAll();
            int lastMonth = activeClients.Where(client => client.Date >= DateTime.Now.AddMonths(-1)).Count();
            int pending = activeClients.Where(client => client.Pending == true).Count();
            return new ClientsStatsDto(activeClients.Count, lastMonth, pending);
        }
    }
}
