namespace Api.Modules.Registrations;

public class RegistrationService : IRegistrationService
{
    private readonly List<RegistrationDto> _registrationsMock;
    private readonly object _lock = new();
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

    public RegistrationsPageDto GetRegistrationsPage(int start, int increment, string sortKey, bool descending)
    {
        var sortProperty = sortKey == "default" ? typeof(RegistrationDto).GetProperty("Id") : typeof(RegistrationDto).GetProperty(sortKey);
        var sortedRegistrations = descending ? _registrationsMock.OrderByDescending(registration => sortProperty.GetValue(registration)) : _registrationsMock.OrderBy(registration => sortProperty.GetValue(registration));
        var page = new RegistrationsPageDto(sortedRegistrations.Skip(start).Take(increment).ToList(), _registrationsMock.Count); 

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
}