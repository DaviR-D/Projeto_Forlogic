using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace Api.Modules.Registrations
{
    public class RegistrationService(List<RegistrationDto> registrations) : IRegistrationService
    {
        private static readonly Lock _lock = new();
        private readonly DtoMapper _dtoMapper = new();

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

        public RegistrationsPageDto GetPagedRegistrations(int start, int increment, List<RegistrationDto> registrations)
        {
            List<RegistrationDto> activeRegistrations = [.. registrations.Where(registration => registration.Deleted == false)];
            List<RegistrationDto> slicedRegistrations = [.. activeRegistrations.Skip(start).Take(increment)];
            List<RegistrationPreviewDto> registrationPreviewList = _dtoMapper.MapPreview(slicedRegistrations);

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

        public RegistrationsPageDto GetSortedRegistrations(string sortKey, bool descending, int start, int increment)
        {
            List<RegistrationDto> activeRegistrations = [.. registrations.Where(registration => registration.Deleted == false)];
            PropertyInfo? sortProperty = sortKey == "default" ? typeof(RegistrationDto).GetProperty("Id") : typeof(RegistrationDto).GetProperty(sortKey, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            IOrderedEnumerable<RegistrationDto> sortedRegistrations = descending ? activeRegistrations.OrderByDescending(sortProperty.GetValue) : activeRegistrations.OrderBy(sortProperty.GetValue);

            return GetPagedRegistrations(start, increment, [.. sortedRegistrations]);
        }

        public RegistrationsPageDto SearchRegistrations(string query, int start, int increment)
        {
            List<RegistrationDto> activeRegistrations = [.. registrations.Where(registration => registration.Deleted == false)];
            List<RegistrationDto> filteredRegistrations = [.. activeRegistrations
                .Where(registration => $"{registration.Name} {registration.Email.Split("@")[0]}"
                .Contains(query, StringComparison.CurrentCultureIgnoreCase))];

            return GetPagedRegistrations(start, increment, filteredRegistrations);
        }
    }
}