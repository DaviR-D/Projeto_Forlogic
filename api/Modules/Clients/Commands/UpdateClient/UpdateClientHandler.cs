using Api.Modules.Repositories;

namespace Api.Modules.Clients.Commands.UpdateClient
{
    public class UpdateClientHandler(ClientRepository repository)
    {
        public void Handle(ClientDto client)
        {
            repository.Update(client);
        }
    }
}
