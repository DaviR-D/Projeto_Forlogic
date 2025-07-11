using System.Globalization;
using System.Reflection;

namespace Api.Modules.Registrations
{
    public class RegistrationService(List<RegistrationDto> registrations) : IRegistrationService
    {
        private static readonly Lock _lock = new();

        public void CreateRegistration(RegistrationDto registration)
        {
            lock (_lock)
            {
                registration.Id = Guid.NewGuid();
                registration.Date = DateTime.Now;
                registrations.Add(registration);
            }
        }
        public RegistrationDto GetSingleRegistration(Guid id)
        {
            RegistrationDto registration = registrations.First(r => r.Id == id);
            return registration;
        }

        public RegistrationsPageDto GetRegistrationsPage(int start, int increment, string sortKey, bool descending, string query)
        {
            List<RegistrationDto> activeRegistrations = FilterDeletedRegistrations(registrations);
            List<RegistrationDto> filteredRegistrations = SearchRegistrations(activeRegistrations, query);
            List<RegistrationDto> sortedRegistrations = SortRegistrations(filteredRegistrations, sortKey, descending);
            List<RegistrationDto> slicedRegistrations = SliceRegistrations(sortedRegistrations, start, increment);
            List<RegistrationPreviewDto> registrationPreviewList = MapPreview(slicedRegistrations);

            int lastMonth = activeRegistrations.Where(registration => registration.Date >= DateTime.Now.AddMonths(-1)).Count();
            int pending = activeRegistrations.Where(registration => registration.Pending == true).Count();
            RegistrationsPageDto page = new(registrationPreviewList, activeRegistrations.Count, lastMonth, pending);

            return page;
        }

        public List<RegistrationDto> GetAllRegistrations()
        {
            return registrations;
        }

        public void UpdateRegistration(RegistrationDto registration)
        {
            int registrationIndex = registrations.FindIndex(r => r.Id == registration.Id);
            registrations[registrationIndex] = registration;
        }

        public void DeleteRegistration(Guid id)
        {
            RegistrationDto registration = registrations.First(r => r.Id == id);
            registration.Deleted = true;
        }

        public bool VerifyAvailableEmail(Guid id, string email)
        {
            RegistrationDto? existingEmail = registrations.FirstOrDefault(registration => registration.Email == email);
            if (existingEmail != null)
            {
                return existingEmail.Id.Equals(id);
            }
            return true;
        }

        public List<RegistrationDto> FilterDeletedRegistrations(List<RegistrationDto> registrations)
        {
            return [.. registrations.Where(registration => registration.Deleted == false)];
        }

        public List<RegistrationDto> SortRegistrations(List<RegistrationDto> registrations, string sortKey, bool descending)
        {
            PropertyInfo? sortProperty = sortKey == "default" ? typeof(RegistrationDto).GetProperty("Id") : typeof(RegistrationDto).GetProperty(sortKey, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            IOrderedEnumerable<RegistrationDto> sortedRegistrations = descending ? registrations.OrderByDescending(sortProperty.GetValue) : registrations.OrderBy(sortProperty.GetValue);

            return [.. sortedRegistrations];
        }

        public List<RegistrationDto> SearchRegistrations(List<RegistrationDto> registrations, string query)
        {
            return [.. registrations
                .Where(registration => $"{registration.Name} {registration.Email.Split("@")[0]}"
                .Contains(query, StringComparison.CurrentCultureIgnoreCase))];
        }

        public List<RegistrationDto> SliceRegistrations(List<RegistrationDto> registrations, int start, int increment)
        {
            return [.. registrations.Skip(start).Take(increment)];
        }

        public List<RegistrationPreviewDto> MapPreview(List<RegistrationDto> registrations)
        {
            return [.. registrations
                .Select(registration =>
                new RegistrationPreviewDto(
                    registration.Id,
                    registration.Name,
                    registration.Email,
                    registration.Status,
                    registration.Date
                    )
                )];
        }
    }
}