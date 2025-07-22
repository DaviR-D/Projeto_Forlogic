using Api.Modules.Authentication.Application.Commands.Authenticate;
using Api.Modules.Authentication.Application.Commands.CreateUser;
using Api.Shared.Interfaces;

namespace Api.Modules.Authentication.Application
{
    public class AuthenticationHandlerFactory(IServiceProvider service)
    {
        private static readonly Dictionary<String, Type> Handlers = new()
        {
            {"Signup", typeof(CreateUserHandler) },
            {"Authenticate", typeof(AuthenticateHandler)}
        };
        public IRequestHandler<IRequestOutput, IRequestInput> GetHandler(string endpoint)
        {
            return (IRequestHandler<IRequestOutput, IRequestInput>)service.GetService(Handlers[endpoint]);
        }
    }
}
