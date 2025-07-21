using Api.Modules.Clients.Domain;
using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Queries.GetSingleClient
{
    public class GetSingleClientResponse(Client client) : IRequestOutput
    {
        public Client Client { get; set; } = client;
    }
}
