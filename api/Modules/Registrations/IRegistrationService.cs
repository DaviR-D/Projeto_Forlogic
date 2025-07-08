namespace Api.Modules.Registrations;

public interface IRegistrationService
{
    void CreateRegistration(RegistrationDto registration);
    RegistrationDto GetSingleRegistration(Guid id);
    List<RegistrationDto> GetAllRegistrations();
    void UpdateRegistration(RegistrationDto registration);
    void DeleteRegistration(Guid id);

}