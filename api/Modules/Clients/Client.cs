namespace Api.Modules.Clients
{
    public class Client(
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
        public Guid Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string Email { get; set; } = email;
        public string Status { get; set; } = status;
        public bool Pending { get; set; } = pending;
        public DateTime Date { get; set; } = date;
        public int Age { get; set; } = age;
        public string Address { get; set; } = address;
        public string Other { get; set; } = other;
        public string Interests { get; set; } = interests;
        public string Feelings { get; set; } = feelings;
        public string Values { get; set; } = values;
        public bool Deleted { get; set; }
    }
}