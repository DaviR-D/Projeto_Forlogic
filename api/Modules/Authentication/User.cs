namespace Api.Modules.Authentication
{
    public class User(string name, string email, string password, string salt)
    {
        public Guid Id { get; set; } = new Guid();
        public string Name { get; set; } = name;
        public string Email { get; set; } = email;
        public string Password { get; set; } = password;
        public string Salt { get; set; } = salt;
    }
}
