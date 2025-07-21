using Api.Modules.Clients.Presentation.ClientDTOs;
using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Commands.CreateClient
{
    public class CreateClientCommand(ClientDto client) : IRequestInput
    {
        public ClientDto Client { get; set; } = client;

    }
}
