using Api.Modules.Clients.Interfaces;

namespace Api.Modules.Clients.Commands.CreateClient
{
    public class CreateClientCommand(ClientDto client) : IClientInput
    {
        public ClientDto Client { get; set; } = client;

    }
}
