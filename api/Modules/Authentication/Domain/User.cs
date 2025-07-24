namespace Api.Modules.Authentication.Domain
{
    public class User(Guid id, string name, string email, string password, string salt)
    {
        public Guid Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string Email { get; set; } = email;
        public string Password { get; set; } = password;
        public string Salt { get; set; } = salt;
    }
}
