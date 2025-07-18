using Api.Modules.Clients.Interfaces;

namespace Api.Modules.Clients.Queries.GetPagedClients
{
    public class GetPagedClientsQuery(int start, int increment) : IClientInput
    {
        public int Start { get; set; } = start;
        public int Increment { get; set; } = increment;
    }
}
