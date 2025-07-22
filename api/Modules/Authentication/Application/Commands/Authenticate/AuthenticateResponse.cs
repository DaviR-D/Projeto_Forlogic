using Api.Shared.Interfaces;

namespace Api.Modules.Authentication.Application.Commands.Authenticate
{
    public class AuthenticateResponse(string? token = null, string? message = null) : IRequestOutput
    {
        public string? Token { get; set; } = token;
        public string? Message { get; set; } = message;
    }
}
