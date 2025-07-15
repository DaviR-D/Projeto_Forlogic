using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Registrations
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController(IRegistrationService service, List<RegistrationDto> registrations) : ControllerBase
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
        public IActionResult GetPage(int start, int increment)
        {
            var page = service.GetPagedRegistrations(start, increment, registrations);
            return Ok(page);
        }

        [HttpGet("page/sorted")]
        public IActionResult GetSortedPage(string sortKey, bool descending, int start, int increment)
        {
            var page = service.GetSortedRegistrations(sortKey, descending, start, increment);
            return Ok(page);
        }

        [HttpGet("page/search")]
        public IActionResult SearchRegistrations(int start, int increment, string query = "")
        {
            var results = service.SearchRegistrations(query, start, increment);
            return Ok(results);
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
}