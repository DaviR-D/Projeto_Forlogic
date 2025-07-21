using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.Modules.Clients.Application.Commands.CreateClient;
using Api.Modules.Clients.Application.Queries.GetPagedClients;
using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Modules.Clients.Presentation.ClientDTOs;
using Api.Modules.Clients.Application.Queries.GetClientsStats;
using Api.Modules.Clients.Application.Queries.GetSortedClients;
using Api.Modules.Clients.Application.Queries.GetSingleClient;
using Api.Modules.Clients.Application.Commands.DeleteClient;
using Api.Modules.Clients.Application.Queries.VerifyAvailableEmail;
using Api.Modules.Clients.Application.Commands.UpdateClient;
using Api.Modules.Clients.Application.Queries.SearchClients;

namespace Api.Modules.Clients.Presentation
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController(ClientRepository repository) : ControllerBase
    {
        [HttpPost]
        public IActionResult Create([FromBody] ClientDto client)
        {
            var handler = new CreateClientHandler(repository);
            handler.Handle(new CreateClientCommand(client));
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetSingle([FromRoute] Guid id)
        {
            var handler = new GetSingleClientHandler(repository);
            var response = handler.Handle(new GetSingleClientQuery(id));
            return Ok(response);
        }

        [HttpGet("stats")]
        public IActionResult GetStats()
        {
            var handler = new GetClientsStatsHandler(repository);
            var response = handler.Handle(new GetClientsStatsQuery());
            return Ok(response);
        }

        [HttpGet("page")]
        public IActionResult GetPage(int start, int increment)
        {
            var handler = new GetPagedClientsHandler(repository);
            var response = handler.Handle(new GetPagedClientsQuery(start, increment));
            return Ok(response);
        }

        [HttpGet("page/sorted")]
        public IActionResult GetSortedPage(string sortKey, bool descending, int start, int increment)
        {
            var handler = new GetSortedClientsHandler(repository);
            var response = handler.Handle(new GetSortedClientsQuery(sortKey, descending, start, increment));
            return Ok(response);
        }

        [HttpGet("page/search")]
        public IActionResult SearchClients(int start, int increment, string query = "")
        {
            var handler = new SearchClientsHandler(repository);
            var response = handler.Handle(new SearchClientsQuery(start, increment, query));
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Update([FromBody] ClientDto client)
        {
            var handler = new UpdateClientHandler(repository);
            handler.Handle(new UpdateClientCommand(client));
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var handler = new DeleteClientHandler(repository);
            handler.Handle(new DeleteClientCommand(id));
            return Ok();
        }

        [HttpGet("checkEmail")]
        public IActionResult CheckEmail(Guid id, string email)
        {
            var handler = new VerifyAvailableEmailHandler(repository);
            var response = handler.Handle(new VerifyAvailableEmailQuery(id, email));
            return Ok(response);
        }
    }
}