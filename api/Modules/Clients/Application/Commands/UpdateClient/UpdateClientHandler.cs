using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Commands.UpdateClient
{
    public class UpdateClientHandler(ClientRepository repository) : IRequestHandler<IRequestOutput, IRequestInput>
    {
        public IRequestOutput Handle(IRequestInput input)
        {
            var command = (UpdateClientCommand)input;
            repository.Update(command.Client);

            return new UpdateClientResponse();
        }
    }
}
