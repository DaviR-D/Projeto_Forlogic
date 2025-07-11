namespace Api.Modules.Registrations
{
    public class RegistrationDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public bool Pending { get; set; }
        public DateTime Date { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string Other { get; set; }
        public string Interests { get; set; }
        public string Feelings { get; set; }
        public string Values { get; set; }
        public bool Deleted { get; set; }

        public RegistrationDto(
        Guid id,
        string name,
        string email,
        string status,
        bool pending,
        DateTime date,
        int age,
        string address,
        string other,
        string interests,
        string feelings,
        string values
        )
        {
            Id = id;
            Name = name;
            Email = email;
            Status = status;
            Pending = pending;
            Date = date;
            Age = age;
            Address = address;
            Other = other;
            Interests = interests;
            Feelings = feelings;
            Values = values;
        }

    }
}