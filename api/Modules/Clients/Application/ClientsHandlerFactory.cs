using Api.Modules.Clients.Application.Commands.CreateClient;
using Api.Modules.Clients.Application.Commands.DeleteClient;
using Api.Modules.Clients.Application.Commands.UpdateClient;
using Api.Modules.Clients.Application.Queries.GetClientsStats;
using Api.Modules.Clients.Application.Queries.GetPagedClients;
using Api.Modules.Clients.Application.Queries.GetSingleClient;
using Api.Modules.Clients.Application.Queries.GetSortedClients;
using Api.Modules.Clients.Application.Queries.SearchClients;
using Api.Modules.Clients.Application.Queries.VerifyAvailableEmail;
using Api.Shared.Interfaces;

namespace Api.Modules.Authentication.Application
{
    public class ClientsHandlerFactory(IServiceProvider service)
    {
        private static readonly Dictionary<String, Type> Handlers = new()
        {
            { "Create", typeof(CreateClientHandler) },
            { "GetSingle", typeof(GetSingleClientHandler) },
            { "GetStats", typeof(GetClientsStatsHandler) },
            { "GetPage", typeof(GetPagedClientsHandler) },
            { "GetSortedPage", typeof(GetSortedClientsHandler) },
            { "SearchClients", typeof(SearchClientsHandler) },
            { "Update", typeof(UpdateClientHandler) },
            { "Delete", typeof(DeleteClientHandler) },
            { "CheckEmail", typeof(VerifyAvailableEmailHandler) }
        };

        public IRequestHandler<IRequestOutput, IRequestInput> GetHandler(string endpoint)
        {
            return (IRequestHandler<IRequestOutput, IRequestInput>)service.GetService(Handlers[endpoint]);
        }
    }
}
