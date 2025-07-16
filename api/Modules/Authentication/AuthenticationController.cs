using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Authentication
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(AuthenticationService service) : ControllerBase
    {
        [HttpPost("signup")]
        public IActionResult Create([FromBody] UserDto user)
        {
            service.CreateUser(user);
            return Ok();
        }

        [HttpPost]
        public ActionResult Authenticate([FromBody] UserDto user)
        {
            var token = service.Authenticate(user);
            if (token != null) return Ok(new { Token = token });
            else return Unauthorized();
        }
    }
}
