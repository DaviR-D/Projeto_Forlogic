using Api.Modules.Authentication.Presentation.UserDTOs;
using Api.Shared.Interfaces;

namespace Api.Modules.Authentication.Application.Commands.CreateUser
{
    public class CreateUserCommand(UserDto user) : IRequestInput
    {
        public UserDto User { get; set; } = user;
    }
}
