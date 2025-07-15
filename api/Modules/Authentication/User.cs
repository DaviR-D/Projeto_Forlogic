namespace Api.Modules.Authentication
{
    public class User(string name, string email, string password)
    {
        public Guid Id { get; set; } = new Guid();
        public string Name { get; set; } = name;
        public string Email { get; set; } = email;
        public string Password { get; set; } = password;
    }
}
