using Api.Modules.Logs.Presentation.LogDTOs;
using Api.Shared.Interfaces;

namespace Api.Modules.Logs.Application.Queries.GetLogs
{
    public class GetLogsResponse( List<LogDto> logs, string? message = null) : IRequestOutput
    {
        public string? Message { get; set; } = message;
        public List<LogDto> Logs { get; set; } = logs;
    }
}
