using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Commands.DeleteClient
{
    public class DeleteClientResponse(string? message = null) : IRequestOutput
    {
        public string? Message { get; set; } = message;
    }
}
