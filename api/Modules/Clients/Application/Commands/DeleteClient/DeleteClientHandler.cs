using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Commands.DeleteClient
{
    public class DeleteClientHandler(ClientRepository repository) : IRequestHandler<IRequestOutput, IRequestInput>
    {
        public IRequestOutput Handle(IRequestInput input)
        {
            var command = (DeleteClientCommand)input;
            repository.Delete(command.Id);

            return new DeleteClientResponse();
        }
    }
}
