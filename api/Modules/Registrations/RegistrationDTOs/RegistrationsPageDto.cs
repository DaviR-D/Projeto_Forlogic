namespace Api.Modules.Registrations
{
    public class RegistrationsPageDto
    {
        public List<RegistrationPreviewDto> registrations { get; set; }
        public int registrationsLength { get; set; }

        public RegistrationsPageDto(List<RegistrationPreviewDto> r, int length)
        {
            registrations = r;
            registrationsLength = length;
        }
    }
}
