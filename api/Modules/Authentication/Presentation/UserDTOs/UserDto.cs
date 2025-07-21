namespace Api.Modules.Authentication.Presentation.UserDTOs
{
    public class UserDto(string? name, string email, string password)
    {
        public string? Name { get; set; } = name;
        public string Email { get; set; } = email;
        public string Password { get; set; } = password;
    }
}
