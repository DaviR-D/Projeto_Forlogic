namespace Api.Modules.Clients.Presentation.ClientDTOs
{
    public class ClientsStatsDto(int length, int lastMonth, int pending)
    {
        public int ClientsLength { get; set; } = length;
        public int LastMonthClients { get; set; } = lastMonth;
        public int PendingClients { get; set; } = pending;
    }
}
