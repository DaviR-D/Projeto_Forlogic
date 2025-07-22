using Api.Modules.Authentication.Application;
using Api.Modules.Authentication.Application.Commands.Authenticate;
using Api.Modules.Authentication.Application.Commands.CreateUser;
using Api.Modules.Authentication.Presentation.UserDTOs;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Authentication.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(AuthenticationHandlerFactory factory) : ControllerBase
    {
        [HttpPost("signup")]
        public IActionResult Create([FromBody] UserDto user)
        {
            var handler = factory.GetHandler("Signup");
            var response = handler.Handle(new CreateUserCommand(user));
            return Ok(response);
        }

        [HttpPost]
        public ActionResult Authenticate([FromBody] UserDto user)
        {
            var handler = factory.GetHandler("Authenticate");
            var response = handler.Handle(new AuthenticateCommand(user));
            if (response.Message == null) return Ok(response);
            else return Unauthorized(response);
        }
    }
}
