using Api.Modules.Logs.Presentation.LogDTOs;
using Api.Shared.Interfaces;

namespace Api.Modules.Logs.Application.Queries.GetLogs
{
    public class GetLogsResponse( List<GetLogDto> logs, string? message = null) : IRequestOutput
    {
        public string? Message { get; set; } = message;
        public List<GetLogDto> Logs { get; set; } = logs;
    }
}
