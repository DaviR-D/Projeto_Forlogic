using Api.Modules.Clients.Domain;
using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Queries.GetClientsStats
{
    public class GetClientsStatsHandler(ClientRepository repository) : IRequestHandler<IRequestOutput, IRequestInput>
    {
        public IRequestOutput Handle(IRequestInput input)
        {
            List<Client> activeClients = repository.GetAll();
            int lastMonth = activeClients.Where(client => client.Date >= DateTime.Now.AddMonths(-1)).Count();
            int pending = activeClients.Where(client => client.Pending == true).Count();
            return new GetClientsStatsResponse(activeClients.Count, lastMonth, pending);
        }
    }
}
