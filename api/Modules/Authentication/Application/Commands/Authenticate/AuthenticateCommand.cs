using Api.Modules.Authentication.Presentation.UserDTOs;
using Api.Shared.Interfaces;

namespace Api.Modules.Authentication.Application.Commands.Authenticate
{
    public class AuthenticateCommand(UserDto user) : IRequestInput
    {
        public UserDto UserCredentials { get; set; } = user;
    }
}
