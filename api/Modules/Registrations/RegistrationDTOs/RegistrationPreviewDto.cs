namespace Api.Modules.Registrations
{
    public class RegistrationPreviewDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }

        public RegistrationPreviewDto(
            Guid id,
            string name,
            string email,
            string status,
            DateTime date
            )
        {
            Id = id;
            Name = name;
            Email = email;
            Status = status;
            Date = date;
        }
    }
}
