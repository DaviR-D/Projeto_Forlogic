using Api.Modules.Clients.Presentation.ClientDTOs;
using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Queries.GetPagedClients
{
    public class GetPagedClientsResponse(List<ClientPreviewDto> page, string? message = null) : IRequestOutput
    {
        public List<ClientPreviewDto> Page { get; set; } = page;
        public string? Message { get; set; } = message;
    }
}
