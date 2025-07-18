using Api.Modules.Repositories;

namespace Api.Modules.Clients.Queries.SearchClients
{
    public class SearchClientsHandler(ClientRepository repository)
    {
        public List<ClientPreviewDto> Handle(string query, int start, int increment)
        {
            List<Client> filteredClients = repository.Search(query);
            List<Client> slicedClients = [.. filteredClients.Skip(start).Take(increment)];
            List<ClientPreviewDto> page = DtoMapper.ToPreviewDto(slicedClients);

            return page;
        }
    }
}
