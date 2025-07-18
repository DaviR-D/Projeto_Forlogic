using Api.Modules.Clients.Interfaces;
using Api.Modules.Repositories;

namespace Api.Modules.Clients.Commands.CreateClient
{
    public class CreateClientHandler(ClientRepository repository) : IClientHandler<IClientOutput, IClientInput>
    {
        private static readonly Lock _lock = new();

        public IClientOutput Handle(IClientInput input)
        {
            var command = (CreateClientCommand)input;
            lock (_lock)
            {
                if (VerifyAvailableEmail(command.Client.Id, command.Client.Email))
                    repository.Create(command.Client);
            }
            return new CreateClientResponse();
        }
        public bool VerifyAvailableEmail(Guid id, string email)
        {
            Client? existingEmail = repository.GetAll().FirstOrDefault(command => command.Email == email);
            if (existingEmail != null)
            {
                return existingEmail.Id.Equals(id);
            }
            return true;
        }
    }
}
