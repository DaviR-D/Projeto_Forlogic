using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Queries.GetPagedClients
{
    public class GetPagedClientsQuery(int start, int increment) : IRequestInput
    {
        public int Start { get; set; } = start;
        public int Increment { get; set; } = increment;
    }
}
