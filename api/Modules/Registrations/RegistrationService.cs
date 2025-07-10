using System.Reflection;

namespace Api.Modules.Registrations
{
    public class RegistrationService : IRegistrationService
    {
        private readonly List<RegistrationDto> _registrationsMock;
        private static readonly object _lock = new();
        public RegistrationService(List<RegistrationDto> registrations)
        {
            _registrationsMock = registrations;
        }
        public void CreateRegistration(RegistrationDto registration)
        {
            lock (_lock)
            {
                registration.Id = Guid.NewGuid();
                _registrationsMock.Add(registration);
            }
        }
        public RegistrationDto GetSingleRegistration(Guid id)
        {
            var registration = _registrationsMock.First(r => r.Id == id);
            return registration;
        }

        public RegistrationsPageDto GetRegistrationsPage(int start, int increment, string sortKey, bool descending, string query)
        {
            var sortProperty = sortKey == "default" ? typeof(RegistrationDto).GetProperty("Id") : typeof(RegistrationDto).GetProperty(sortKey, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            var sortedRegistrations = descending ? _registrationsMock.OrderByDescending(registration => sortProperty.GetValue(registration)) : _registrationsMock.OrderBy(registration => sortProperty.GetValue(registration));
            var registrationList = sortedRegistrations
                .Where(registration => $"{registration.Name} {registration.Email.Split("@")[0]}"
                .ToLower()
                .Contains(query))
                .Skip(start)
                .Take(increment)
                .ToList();
            List<RegistrationPreviewDto> registrationPreviewList = registrationList
                .Select(registration =>
                new RegistrationPreviewDto(
                    registration.Id,
                    registration.Name,
                    registration.Email,
                    registration.Status,
                    registration.Date))
                .ToList();
            var page = new RegistrationsPageDto(registrationPreviewList, _registrationsMock.Count);

            return page;
        }

        public List<RegistrationDto> GetAllRegistrations()
        {
            return _registrationsMock;
        }
        public void UpdateRegistration(RegistrationDto registration)
        {
            var registrationIndex = _registrationsMock.FindIndex(r => r.Id == registration.Id);
            _registrationsMock[registrationIndex] = registration;
        }
        public void DeleteRegistration(Guid id)
        {
            var registration = _registrationsMock.First(r => r.Id == id);
            _registrationsMock.Remove(registration);
        }
        public bool VerifyAvailableEmail(Guid id, string email)
        {
            var existingEmail = _registrationsMock.FirstOrDefault(registration => registration.Email == email);
            if (existingEmail != null)
            {
                return existingEmail.Id.Equals(id);
            }
            return true;
        }
    }
}