namespace Api.Modules.Registrations
{
    public class RegistrationsPageDto
    {
        public List<RegistrationPreviewDto> Registrations { get; set; }
        public int RegistrationsLength { get; set; }
        public int LastMonthRegistrations { get; set; }
        public int PendingRegistrations { get; set; }

        public RegistrationsPageDto(List<RegistrationPreviewDto> registrations, int length, int lastMonth, int pending)
        {
            Registrations = registrations;
            RegistrationsLength = length;
            LastMonthRegistrations = lastMonth;
            PendingRegistrations = pending;
        }
    }
}
