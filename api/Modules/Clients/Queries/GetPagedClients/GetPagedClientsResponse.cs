using Api.Modules.Clients.Interfaces;

namespace Api.Modules.Clients.Queries.GetPagedClients
{
    public class GetPagedClientsResponse(List<ClientPreviewDto> page) : IClientOutput
    {
        public List<ClientPreviewDto> Page { get; set; } = page;
    }
}
