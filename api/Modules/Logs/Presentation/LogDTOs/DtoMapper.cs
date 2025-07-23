using Api.Modules.Logs.Domain;

namespace Api.Modules.Logs.Presentation.LogDTOs
{
    public class DtoMapper
    {
        public static Log ToEntity(LogDto log)
        {
            return new Log(
                id:(Guid)log.Id,
                userId:log.UserId,
                clientId:log.ClientId,
                timeStamp:(DateTime)log.TimeStamp,
                action:log.Action
                );
        }
        public static LogDto ToDto(Log log)
        {
            return new LogDto(
                id:log.Id,
                userId: log.UserId,
                clientId: log.ClientId,
                timeStamp: log.TimeStamp,
                action: log.Action
                );
        }
    }
}
