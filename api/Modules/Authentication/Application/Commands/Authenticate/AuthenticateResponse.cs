using Api.Shared.Interfaces;

namespace Api.Modules.Authentication.Application.Commands.Authenticate
{
    public class AuthenticateResponse(string token) : IRequestOutput
    {
        public string Token { get; set; } = token;
    }
}
