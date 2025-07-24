namespace Api.Modules.Logs.Presentation.LogDTOs
{
    public class CreateLogDto(Guid? id, Guid? userId, Guid clientId, DateTime? timeStamp, string action)
    {
        public Guid? Id { get; set; } = id;
        public Guid? UserId { get; set; } = userId;
        public Guid ClientId { get; set; } = clientId;
        public DateTime? TimeStamp { get; set; } = timeStamp;
        public string Action { get; set; } = action;
    }
}