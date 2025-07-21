using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Queries.GetSingleClient
{
    public class GetSingleClientQuery(Guid id) : IRequestInput
    {
        public Guid Id { get; set; } = id;
    }
}
