namespace Api.Modules.Registrations
{
    public interface IRegistrationService
    {
        void CreateRegistration(RegistrationDto registration);
        RegistrationDto GetSingleRegistration(Guid id);
        List<RegistrationPreviewDto> GetPagedRegistrations(int start, int increment, List<Registration> registrations);
        List<RegistrationPreviewDto> GetSortedRegistrations(string sortKey, bool descending, int start, int increment);
        List<RegistrationPreviewDto> SearchRegistrations(string query, int start, int increment);
        void UpdateRegistration(RegistrationDto registration);
        void DeleteRegistration(Guid id);
        bool VerifyAvailableEmail(Guid id, string email);
        RegistrationsStatsDto GetRegistrationsStats();
    }
}