using Api.Modules.Clients.Presentation.ClientDTOs;
using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Commands.UpdateClient
{
    public class UpdateClientCommand(ClientDto client) : IRequestInput
    {
        public ClientDto Client { get; set; } = client;
    }
}
