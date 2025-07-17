using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Api.Modules.Authentication
{
    public class AuthenticationService(List<User> users, AuthenticationSettings auth)
    {
        public void CreateUser(UserDto user)
        {
            string salt = new Guid().ToString();
            string password = user.Password + salt;
            byte[] encodedPassword = Encoding.UTF8.GetBytes(password);
            byte[] passwordHash = SHA256.HashData(encodedPassword);

            User newUser = new(user.Name, user.Email, Convert.ToBase64String(passwordHash), salt);
            users.Add(newUser);
        }
        public string? Authenticate(UserDto userCredentials)
        {
            var user = users.FirstOrDefault(user => userCredentials.Email == user.Email);

            if (user != null)
            {
                string salt = user.Salt;
                string password = userCredentials.Password + salt;
                byte[] encodedPassword = Encoding.UTF8.GetBytes(password);
                byte[] passwordHash = SHA256.HashData(encodedPassword);

                if (Convert.ToBase64String(passwordHash) == user.Password) return GenerateToken(user);
            }
            return null;
        }
        private string GenerateToken(User user)
        {
            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(auth.PrivateKey);

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                algorithm: SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaims(user),
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(1),
            };

            var token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }
        private static ClaimsIdentity GenerateClaims(User user)
        {
            var claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaim(new Claim(type: ClaimTypes.Name, value: user.Name));

            return claimsIdentity;
        }
        public bool VerifyAvailableEmail(Guid id, string email)
        {
            User? existingEmail = users.FirstOrDefault(user => user.Email == email);
            if (existingEmail != null)
            {
                return existingEmail.Id.Equals(id);
            }
            return true;
        }
    }
}
