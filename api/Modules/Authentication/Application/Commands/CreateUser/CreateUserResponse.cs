using Api.Shared.Interfaces;

namespace Api.Modules.Authentication.Application.Commands.CreateUser
{
    public class CreateUserResponse(string? message = null) : IRequestOutput
    {
        public string? Message { get; set; } = message;
    }
}
