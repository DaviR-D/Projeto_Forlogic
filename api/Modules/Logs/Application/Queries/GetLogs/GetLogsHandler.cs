using Api.Modules.Logs.Infrastructure.Repositories;
using Api.Modules.Logs.Presentation.LogDTOs;
using Api.Shared.Interfaces;

namespace Api.Modules.Logs.Application.Queries.GetLogs
{
    public class GetLogsHandler(LogRepository repository) : IRequestHandler<IRequestOutput, IRequestInput>
    {
        public IRequestOutput Handle(IRequestInput input)
        {
            return new GetLogsResponse(logs: [.. repository.GetAll().Select(log => DtoMapper.ToDto(log))]);
        }
    }
}
