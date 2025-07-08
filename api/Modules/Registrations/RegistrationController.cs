using Microsoft.AspNetCore.Mvc;
namespace Api.Modules.Registrations;

[ApiController]
[Route("api/[controller]")]
public class RegistrationController : ControllerBase
{
    private readonly IRegistrationService _service;

    public RegistrationController(IRegistrationService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult Create([FromBody] RegistrationDto registration)
    {
        _service.CreateRegistration(registration);
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult GetSingle([FromRoute] Guid id)
    {
        var registration = _service.GetSingleRegistration(id);
        return Ok(registration);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var registrations = _service.GetAllRegistrations();
        return Ok(registrations);
    }

    [HttpPut]
    public IActionResult Update([FromBody] RegistrationDto registration)
    {
        _service.UpdateRegistration(registration);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] Guid id)
    {
        _service.DeleteRegistration(id);
        return Ok();
    }
}