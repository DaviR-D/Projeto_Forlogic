using Api.Modules.Authentication.Domain;
using Api.Modules.Authentication.Infrastructure.Repositories;
using Api.Modules.Authentication.Presentation.UserDTOs;
using Api.Shared.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Api.Modules.Authentication.Application.Commands.CreateUser
{
    public class CreateUserHandler(UserRepository repository) : IRequestHandler<IRequestOutput, IRequestInput>
    {
        public IRequestOutput Handle(IRequestInput input)
        {
            var command = (CreateUserCommand)input;
            var user = command.User;
            if (!VerifyAvailableEmail(user.Email)) return new CreateUserResponse("email already in use");

            string salt = new Guid().ToString();
            string password = user.Password + salt;
            byte[] encodedPassword = Encoding.UTF8.GetBytes(password);
            byte[] passwordHash = SHA256.HashData(encodedPassword);

            User newUser = new(user.Name, user.Email, Convert.ToBase64String(passwordHash), salt);
            repository.Create(newUser);

            return new CreateUserResponse();
        }
        public bool VerifyAvailableEmail(string email)
        {
            User? existingEmail = repository.GetAll().FirstOrDefault(user => user.Email == email);
            if (existingEmail != null) return false;
            return true;
        }
    }
}
