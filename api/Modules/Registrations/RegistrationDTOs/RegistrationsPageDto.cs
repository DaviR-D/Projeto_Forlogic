namespace Api.Modules.Registrations
{
    public class RegistrationsPageDto(List<RegistrationPreviewDto> registrations, int length, int lastMonth, int pending)
    {
        public List<RegistrationPreviewDto> Registrations { get; set; } = registrations;
        public int RegistrationsLength { get; set; } = length;
        public int LastMonthRegistrations { get; set; } = lastMonth;
        public int PendingRegistrations { get; set; } = pending;
    }
}
