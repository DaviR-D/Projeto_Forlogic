using Api.Modules.Clients.Domain;
using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Modules.Clients.Presentation.ClientDTOs;
using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Queries.SearchClients
{
    public class SearchClientsHandler(ClientRepository repository) : IRequestHandler<IRequestOutput, IRequestInput>
    {
        public IRequestOutput Handle(IRequestInput input)
        {
            var query = (SearchClientsQuery)input;
            List<Client> filteredClients = repository.Search(query.Query);
            List<Client> slicedClients = [.. filteredClients.Skip(query.Start).Take(query.Increment)];
            var response = new SearchClientsResponse(DtoMapper.ToPreviewDto(slicedClients));

            return response;
        }
    }
}
