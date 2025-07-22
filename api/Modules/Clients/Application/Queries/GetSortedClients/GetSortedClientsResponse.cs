using Api.Modules.Clients.Presentation.ClientDTOs;
using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Queries.GetSortedClients
{
    public class GetSortedClientsResponse(List<ClientPreviewDto> page, string? message = null) : IRequestOutput
    {
        public List<ClientPreviewDto> Page { get; set; } = page;
        public string? Message { get; set; } = message;
    }
}
