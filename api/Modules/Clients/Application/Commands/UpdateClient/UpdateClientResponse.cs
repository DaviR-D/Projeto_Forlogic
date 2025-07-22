using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Commands.UpdateClient
{
    public class UpdateClientResponse(string? message = null) : IRequestOutput
    {
        public string? Message { get; set; } = message;
    }
}
