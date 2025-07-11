using Microsoft.AspNetCore.Mvc;
namespace Api.Modules.Registrations;

[ApiController]
[Route("api/[controller]")]
public class RegistrationController(IRegistrationService service) : ControllerBase
{
    [HttpPost]
    public IActionResult Create([FromBody] RegistrationDto registration)
    {
        service.CreateRegistration(registration);
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult GetSingle([FromRoute] Guid id)
    {
        var registration = service.GetSingleRegistration(id);
        return Ok(registration);
    }

    [HttpGet("page")]
    public IActionResult GetPage(int start, int increment, string sortKey, bool descending, string query = "")
    {
        var page = service.GetRegistrationsPage(start, increment, sortKey, descending, query);
        return Ok(page);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var registrations = service.GetAllRegistrations();
        return Ok(registrations);
    }

    [HttpPut]
    public IActionResult Update([FromBody] RegistrationDto registration)
    {
        service.UpdateRegistration(registration);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] Guid id)
    {
        service.DeleteRegistration(id);
        return Ok();
    }

    [HttpGet("checkEmail")]
    public IActionResult CheckEmail(Guid id, string email)
    {
        var emailAvailable = service.VerifyAvailableEmail(id, email);
        return Ok(emailAvailable);
    }
}