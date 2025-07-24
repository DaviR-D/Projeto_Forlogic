using Api.Modules.Logs.Presentation.LogDTOs;
using Api.Shared.Interfaces;

namespace Api.Modules.Logs.Application.Commands.CreateLog
{
    public class CreateLogCommand(CreateLogDto log, Guid userId) : IRequestInput
    {
        public CreateLogDto Log = log;
        public Guid UserId { get; set; } = userId; 
    }
}
