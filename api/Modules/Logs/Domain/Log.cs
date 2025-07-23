namespace Api.Modules.Logs.Domain
{
    public class Log(Guid id, Guid userId, Guid clientId, DateTime timeStamp, string action)
    {
        public Guid Id { get; set; } = id;
        public Guid UserId { get; set; } = userId;
        public Guid ClientId { get; set; } = clientId;
        public DateTime TimeStamp { get; set; } = timeStamp;
        public string Action { get; set; } = action;
    }
}
