namespace Api.Modules.Registrations
{
    public interface IRegistrationService
    {
        void CreateRegistration(RegistrationDto registration);
        RegistrationDto GetSingleRegistration(Guid id);
        RegistrationsPageDto GetRegistrationsPage(int start, int increment, string sortKey, bool descending, string query);
        List<RegistrationDto> GetAllRegistrations();
        void UpdateRegistration(RegistrationDto registration);
        void DeleteRegistration(Guid id);

        bool VerifyAvailableEmail(Guid id, string email);

    }
}