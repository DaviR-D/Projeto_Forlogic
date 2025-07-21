using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Commands.DeleteClient
{
    public class DeleteClientCommand(Guid id) : IRequestInput
    {
        public Guid Id { get; set; } = id;
    }
}
