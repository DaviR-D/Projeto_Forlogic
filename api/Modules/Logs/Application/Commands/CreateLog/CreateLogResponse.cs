using Api.Shared.Interfaces;

namespace Api.Modules.Logs.Application.Commands.CreateLog
{
    public class CreateLogResponse(string? message = null) : IRequestOutput
    {
        public string? Message { get; set; } = message;
    }
}
