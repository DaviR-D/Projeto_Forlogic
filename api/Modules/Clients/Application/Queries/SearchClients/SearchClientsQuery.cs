using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Queries.SearchClients
{
    public class SearchClientsQuery(int start, int increment, string query) : IRequestInput
    {
        public int Start { get; set; } = start;
        public int Increment { get; set; } = increment;
        public string Query { get; set; } = query;
    }
}
