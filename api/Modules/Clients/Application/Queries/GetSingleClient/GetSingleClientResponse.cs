using Api.Modules.Clients.Domain;
using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Queries.GetSingleClient
{
    public class GetSingleClientResponse(Client client, string? message = null) : IRequestOutput
    {
        public Client Client { get; set; } = client;
        public string? Message { get; set; } = message;
    }
}
