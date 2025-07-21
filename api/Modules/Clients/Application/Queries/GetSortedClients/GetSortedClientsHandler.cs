using Api.Modules.Clients.Domain;
using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Modules.Clients.Presentation.ClientDTOs;
using Api.Shared.Interfaces;
using System.Reflection;

namespace Api.Modules.Clients.Application.Queries.GetSortedClients
{
    public class GetSortedClientsHandler(ClientRepository repository) : IRequestHandler<IRequestOutput, IRequestInput>
    {
        public IRequestOutput Handle(IRequestInput input)
        {
            var query = (GetSortedClientsQuery)input;
            List<Client> sortedClients = repository.Sort(query.SortKey, query.Descending);
            List<Client> slicedClients = [.. sortedClients.Skip(query.Start).Take(query.Increment)];
            var response = new GetSortedClientsResponse(DtoMapper.ToPreviewDto(slicedClients));

            return response;
        }
    }
}
