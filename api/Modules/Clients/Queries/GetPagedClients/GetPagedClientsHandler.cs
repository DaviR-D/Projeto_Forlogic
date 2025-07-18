using Api.Modules.Clients.Interfaces;
using Api.Modules.Repositories;

namespace Api.Modules.Clients.Queries.GetPagedClients
{
    public class GetPagedClientsHandler(ClientRepository repository) : IClientHandler<IClientOutput, IClientInput>
    {
        public IClientOutput Handle(IClientInput input)
        {
            var query = (GetPagedClientsQuery)input;
            List<Client> activeClients = repository.GetAll();
            List<Client> slicedClients = [.. activeClients.Skip(query.Start).Take(query.Increment)];
            var response = new GetPagedClientsResponse(DtoMapper.ToPreviewDto(slicedClients));
            
            return response;
        }
    }
}
