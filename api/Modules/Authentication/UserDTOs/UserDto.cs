﻿namespace Api.Modules.Authentication
{
    public class UserDto(string email, string password)
    {
        public string Email { get; set; } = email;
        public string Password { get; set; } = password;
    }
}
