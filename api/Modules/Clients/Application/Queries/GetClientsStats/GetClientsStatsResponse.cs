using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Queries.GetClientsStats
{
    public class GetClientsStatsResponse(int length, int lastMonth, int pending, string? message = null) : IRequestOutput
    {
        public int ClientsLength { get; set; } = length;
        public int LastMonthClients { get; set; } = lastMonth;
        public int PendingClients { get; set; } = pending;
        public string? Message { get; set; } = message;
    }
}
