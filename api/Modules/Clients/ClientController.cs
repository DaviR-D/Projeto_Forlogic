using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.Modules.Repositories;
using Api.Modules.Clients.Interfaces;
using Api.Modules.Clients.Commands.CreateClient;
using Api.Modules.Clients.Queries.GetPagedClients;

namespace Api.Modules.Clients
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

        //[HttpGet("{id}")]
        //public IActionResult GetSingle([FromRoute] Guid id)
        //{
        //    var client = handler.Handle(id);
        //    return Ok(client);
        //}

        //[HttpGet("stats")]
        //public IActionResult GetStats()
        //{
        //    return Ok(handler.Handle());
        //}

        [HttpGet("page")]
        public IActionResult GetPage(int start, int increment)
        {
            var handler = new GetPagedClientsHandler(repository);
            var response = handler.Handle(new GetPagedClientsQuery(start, increment));
            var page = (GetPagedClientsResponse)response;
            return Ok(page.Page);
        }

        //[HttpGet("page/sorted")]
        //public IActionResult GetSortedPage(string sortKey, bool descending, int start, int increment)
        //{
        //    var page = handler.Handle(sortKey, descending, start, increment);
        //    return Ok(page);
        //}

        //[HttpGet("page/search")]
        //public IActionResult SearchClients(int start, int increment, string query = "")
        //{
        //    var results = handler.Handle(query, start, increment);
        //    return Ok(results);
        //}

        //[HttpPut]
        //public IActionResult Update([FromBody] ClientDto client)
        //{
        //    handler.Handle(client);
        //    return Ok();
        //}

        //[HttpDelete("{id}")]
        //public IActionResult Delete([FromRoute] Guid id)
        //{
        //    handler.Handle(id);
        //    return Ok();
        //}

        //[HttpGet("checkEmail")]
        //public IActionResult CheckEmail(Guid id, string email)
        //{
        //    var emailAvailable = handler.VerifyAvailableEmail(id, email);
        //    return Ok(emailAvailable);
        //}
    }
}