using Api.Modules.Clients.Presentation.ClientDTOs;
using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Queries.SearchClients
{
    public class SearchClientsResponse(List<ClientPreviewDto> page, int resultsLength, string? message = null) : IRequestOutput
    {
        public List<ClientPreviewDto> Page { get; set; } = page;
        public int ResultsLength { get; set; } = resultsLength;
        public string? Message { get; set; } = message;
    }
}
