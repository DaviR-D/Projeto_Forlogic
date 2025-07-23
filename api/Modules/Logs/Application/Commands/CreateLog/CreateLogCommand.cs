using Api.Modules.Logs.Presentation.LogDTOs;
using Api.Shared.Interfaces;

namespace Api.Modules.Logs.Application.Commands.CreateLog
{
    public class CreateLogCommand(LogDto log) : IRequestInput
    {
        public LogDto Log = log;
    }
}
