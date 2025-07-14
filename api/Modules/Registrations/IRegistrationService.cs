namespace Api.Modules.Registrations
{
    public interface IRegistrationService
    {
        void CreateRegistration(RegistrationDto registration);
        RegistrationDto GetSingleRegistration(Guid id);
        RegistrationsPageDto GetPagedRegistrations(int start, int increment, List<RegistrationDto> registrations);
        RegistrationsPageDto GetSortedRegistrations(string sortKey, bool descending, int start, int increment);
        RegistrationsPageDto SearchRegistrations(string query, int start, int increment);
        List<RegistrationDto> GetAllRegistrations();
        void UpdateRegistration(RegistrationDto registration);
        void DeleteRegistration(Guid id);

        bool VerifyAvailableEmail(Guid id, string email);

    }
}