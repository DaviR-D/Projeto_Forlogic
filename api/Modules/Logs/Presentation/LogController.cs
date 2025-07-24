using Api.Modules.Logs.Application;
using Api.Modules.Logs.Application.Commands.CreateLog;
using Api.Modules.Logs.Application.Queries.GetLogs;
using Api.Modules.Logs.Presentation.LogDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Modules.Logs.Presentation
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LogController(LogHandlerFactory factory) : ControllerBase
    {
        [HttpPost]
        public IActionResult Create([FromBody] CreateLogDto log)
        {
            var handler = factory.GetHandler("Create");
            var response = handler.Handle(new CreateLogCommand(
                log:log,
                userId:Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value))
                );
            return Ok(response);
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var handler = factory.GetHandler("GetAll");
            var response = handler.Handle(new GetLogsQuery());
            return Ok(response);
        }
    }
}
