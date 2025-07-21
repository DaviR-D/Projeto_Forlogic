using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Modules.Clients.Presentation.ClientDTOs;
using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Queries.GetSingleClient
{
    public class GetSingleClientHandler(ClientRepository repository) : IRequestHandler<IRequestOutput, IRequestInput>
    {
        public IRequestOutput Handle(IRequestInput input)
        {
            var query = (GetSingleClientQuery)input;
            var response = new GetSingleClientResponse(repository.GetOne(query.Id)); 
            return response;
        }
    }
}
