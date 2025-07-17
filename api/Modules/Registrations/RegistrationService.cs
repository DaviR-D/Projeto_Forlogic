using System.Reflection;

namespace Api.Modules.Registrations
{
    public class RegistrationService(List<Registration> registrations) : IRegistrationService
    {
        private static readonly Lock _lock = new();

        public void CreateRegistration(RegistrationDto registration)
        {
            lock (_lock)
            {
                registration.Id = Guid.NewGuid();
                registration.Date = DateTime.Now;
                if(VerifyAvailableEmail(registration.Id, registration.Email)) registrations.Add(DtoMapper.ToEntity(registration));
            }
        }
        public RegistrationDto GetSingleRegistration(Guid id)
        {
            RegistrationDto registration = DtoMapper.ToDto(registrations.First(r => r.Id == id));
            return registration;
        }

        public List<RegistrationPreviewDto> GetPagedRegistrations(int start, int increment, List<Registration> registrations)
        {
            List<Registration> activeRegistrations = [.. registrations.Where(registration => registration.Deleted == false)];
            List<Registration> slicedRegistrations = [.. activeRegistrations.Skip(start).Take(increment)];
            List<RegistrationPreviewDto> page = DtoMapper.ToPreviewDto(slicedRegistrations);

            return page;
        }

        public void UpdateRegistration(RegistrationDto registration)
        {
            int registrationIndex = registrations.FindIndex(r => r.Id == registration.Id);
            registrations[registrationIndex] = DtoMapper.ToEntity(registration);
        }

        public void DeleteRegistration(Guid id)
        {
            Registration registration = registrations.First(r => r.Id == id);
            registration.Deleted = true;
        }

        public bool VerifyAvailableEmail(Guid id, string email)
        {
            Registration? existingEmail = registrations.FirstOrDefault(registration => registration.Email == email);
            if (existingEmail != null)
            {
                return existingEmail.Id.Equals(id);
            }
            return true;
        }

        public List<RegistrationPreviewDto> GetSortedRegistrations(string sortKey, bool descending, int start, int increment)
        {
            List<Registration> activeRegistrations = [.. registrations.Where(registration => registration.Deleted == false)];
            PropertyInfo? sortProperty = sortKey == "default" ? typeof(Registration).GetProperty("Id") : typeof(Registration).GetProperty(sortKey, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            IOrderedEnumerable<Registration> sortedRegistrations = descending ? activeRegistrations.OrderByDescending(sortProperty.GetValue) : activeRegistrations.OrderBy(sortProperty.GetValue);

            return GetPagedRegistrations(start, increment, [.. sortedRegistrations]);
        }

        public List<RegistrationPreviewDto> SearchRegistrations(string query, int start, int increment)
        {
            List<Registration> activeRegistrations = [.. registrations.Where(registration => registration.Deleted == false)];
            List<Registration> filteredRegistrations = [.. activeRegistrations
                .Where(registration => $"{registration.Name} {registration.Email.Split("@")[0]}"
                .Contains(query, StringComparison.CurrentCultureIgnoreCase))];

            return GetPagedRegistrations(start, increment, filteredRegistrations);
        }
        public RegistrationsStatsDto GetRegistrationsStats()
        {
            List<Registration> activeRegistrations = [.. registrations.Where(registration => registration.Deleted == false)];
            int lastMonth = activeRegistrations.Where(registration => registration.Date >= DateTime.Now.AddMonths(-1)).Count();
            int pending = activeRegistrations.Where(registration => registration.Pending == true).Count();
            return new RegistrationsStatsDto(activeRegistrations.Count, lastMonth, pending);
        }
    }
}