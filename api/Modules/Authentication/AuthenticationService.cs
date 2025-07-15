using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Api.Modules.Authentication
{
    public class AuthenticationService(List<User> users)
    {
        public string? Authenticate(UserDto userCredentials)
        {
            foreach (var user in users)
            {
                if (userCredentials.Email == user.Email)
                {
                    if (userCredentials.Password == user.Password)
                    {
                        return GenerateToken(user);
                    }
                }
            }
            return null;
        }
        private static string GenerateToken(User user)
        {
            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(AuthenticationSettings.PrivateKey);

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
    }
}
