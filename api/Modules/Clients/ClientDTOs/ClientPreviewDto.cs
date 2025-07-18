namespace Api.Modules.Clients
{
    public class ClientPreviewDto(
        Guid id,
        string name,
        string email,
        string status,
        DateTime date
            )
    {
        public Guid Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string Email { get; set; } = email;
        public string Status { get; set; } = status;
        public DateTime Date { get; set; } = date;
    }
}
