using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.Modules.Clients.Application.Commands.CreateClient;
using Api.Modules.Clients.Application.Queries.GetPagedClients;
using Api.Modules.Clients.Presentation.ClientDTOs;
using Api.Modules.Clients.Application.Queries.GetClientsStats;
using Api.Modules.Clients.Application.Queries.GetSortedClients;
using Api.Modules.Clients.Application.Queries.GetSingleClient;
using Api.Modules.Clients.Application.Commands.DeleteClient;
using Api.Modules.Clients.Application.Queries.VerifyAvailableEmail;
using Api.Modules.Clients.Application.Commands.UpdateClient;
using Api.Modules.Clients.Application.Queries.SearchClients;
using Api.Modules.Authentication.Application;

namespace Api.Modules.Clients.Presentation
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController(ClientsHandlerFactory factory) : ControllerBase
    {
        [HttpPost]
        public IActionResult Create([FromBody] ClientDto client)
        {
            var handler = factory.GetHandler("Create");
            var response = handler.Handle(new CreateClientCommand(client));
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetSingle([FromRoute] Guid id)
        {
            var handler = factory.GetHandler("GetSingle");
            var response = handler.Handle(new GetSingleClientQuery(id));
            return Ok(response);
        }

        [HttpGet("stats")]
        public IActionResult GetStats()
        {
            var handler = factory.GetHandler("GetStats");
            var response = handler.Handle(new GetClientsStatsQuery());
            return Ok(response);
        }

        [HttpGet("page")]
        public IActionResult GetPage(int start, int increment)
        {
            var handler = factory.GetHandler("GetPage");
            var response = handler.Handle(new GetPagedClientsQuery(start, increment));
            return Ok(response);
        }

        [HttpGet("page/sorted")]
        public IActionResult GetSortedPage(string sortKey, bool descending, int start, int increment)
        {
            var handler = factory.GetHandler("GetSortedPage");
            var response = handler.Handle(new GetSortedClientsQuery(sortKey, descending, start, increment));
            return Ok(response);
        }

        [HttpGet("page/search")]
        public IActionResult SearchClients(int start, int increment, string query = "")
        {
            var handler = factory.GetHandler("SearchClients");
            var response = handler.Handle(new SearchClientsQuery(start, increment, query));
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Update([FromBody] ClientDto client)
        {
            var handler = factory.GetHandler("Update");
            var response = handler.Handle(new UpdateClientCommand(client));
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var handler = factory.GetHandler("Delete");
            var response = handler.Handle(new DeleteClientCommand(id));
            return Ok(response);
        }

        [HttpGet("checkEmail")]
        public IActionResult CheckEmail(Guid id, string email)
        {
            var handler = factory.GetHandler("CheckEmail");
            var response = handler.Handle(new VerifyAvailableEmailQuery(id, email));
            return Ok(response);
        }
    }
}