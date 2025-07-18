using Api.Modules.Repositories;

namespace Api.Modules.Clients.Queries.GetSingleClient
{
    public class GetSingleClientHandler(ClientRepository repository)
    {
        public ClientDto Handle(Guid id)
        {
            return repository.GetOne(id);
        }
    }
}
