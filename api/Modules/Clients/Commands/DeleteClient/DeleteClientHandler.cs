using Api.Modules.Repositories;

namespace Api.Modules.Clients.Commands.DeleteClient
{
    public class DeleteClientHandler(ClientRepository repository)
    {
        public void Handle(Guid id)
        {
            repository.Delete(id);
        }
    }
}
