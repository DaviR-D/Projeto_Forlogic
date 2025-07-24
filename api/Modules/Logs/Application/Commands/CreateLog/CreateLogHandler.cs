using Api.Modules.Logs.Infrastructure.Repositories;
using Api.Modules.Logs.Presentation.LogDTOs;
using Api.Shared.Interfaces;

namespace Api.Modules.Logs.Application.Commands.CreateLog
{
    public class CreateLogHandler(LogRepository repository) : IRequestHandler<IRequestOutput, IRequestInput>
    {
        public IRequestOutput Handle(IRequestInput input) 
        {
            var command = (CreateLogCommand)input;
            command.Log.Id = Guid.NewGuid();
            command.Log.UserId = command.UserId;
            command.Log.TimeStamp = DateTime.Now;
            repository.Create(LogDtoMapper.ToEntity(command.Log));
            return new CreateLogResponse();
        }
    }
}
