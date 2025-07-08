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