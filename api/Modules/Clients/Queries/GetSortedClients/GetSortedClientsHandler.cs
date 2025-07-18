using Api.Modules.Repositories;
using System.Reflection;

namespace Api.Modules.Clients.Queries.GetSortedClients
{
    public class GetSortedClientsHandler(ClientRepository repository)
    {
        public List<ClientPreviewDto> Handle(string sortKey, bool descending, int start, int increment)
        {
            List<Client> sortedClients = repository.Sort(sortKey, descending);
            List<Client> slicedClients = [.. sortedClients.Skip(start).Take(increment)];
            List<ClientPreviewDto> page = DtoMapper.ToPreviewDto(slicedClients);

            return page;
        }
    }
}
