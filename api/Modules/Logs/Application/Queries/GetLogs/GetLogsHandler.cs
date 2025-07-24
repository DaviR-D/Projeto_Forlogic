using Api.Modules.Authentication.Infrastructure.Repositories;
using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Modules.Logs.Infrastructure.Repositories;
using Api.Modules.Logs.Presentation.LogDTOs;
using Api.Shared.Interfaces;

namespace Api.Modules.Logs.Application.Queries.GetLogs
{
    public class GetLogsHandler(LogRepository logRepository, ClientRepository clients, UserRepository users) : IRequestHandler<IRequestOutput, IRequestInput>
    {
        public IRequestOutput Handle(IRequestInput input)
        {
            return new GetLogsResponse(logs: [.. logRepository.GetAll().Select(log => LogDtoMapper.ToResponseDto(log, clients, users))]);
        }
    }
}
