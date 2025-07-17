namespace Api.Modules.Registrations
{
    public class RegistrationsStatsDto(int length, int lastMonth, int pending)
    {
        public int RegistrationsLength { get; set; } = length;
        public int LastMonthRegistrations { get; set; } = lastMonth;
        public int PendingRegistrations { get; set; } = pending;
    }
}
