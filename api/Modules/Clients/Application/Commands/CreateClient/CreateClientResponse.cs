using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Commands.CreateClient
{
    public class CreateClientResponse(string? message = null) : IRequestOutput
    {
        public string? Message { get; set; } = message;
    }
}
