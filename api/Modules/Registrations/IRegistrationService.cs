namespace Api.Modules.Registrations;

public interface IRegistrationService
{
    void CreateRegistration(RegistrationDto registration);
    RegistrationDto GetSingleRegistration(Guid id);
    RegistrationsPageDto GetRegistrationsPage(int start, int increment, string sortKey, bool descending);
    List<RegistrationDto> GetAllRegistrations();
    void UpdateRegistration(RegistrationDto registration);
    void DeleteRegistration(Guid id);

}