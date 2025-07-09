namespace Api.Modules.Registrations;
    public class RegistrationsPageDto
    {
        public List<RegistrationDto> registrations { get; set; }
    public int registrationsLength { get; set; }

    public RegistrationsPageDto(List<RegistrationDto> r, int length)
    {
        registrations = r;
        registrationsLength = length;
    }
    }
