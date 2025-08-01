﻿using Api.Modules.Authentication.Domain;
using Api.Modules.Authentication.Infrastructure.Repositories;
using Api.Shared.Configurations;
using Api.Shared.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Api.Modules.Authentication.Application.Commands.Authenticate
{
    public class AuthenticateHandler(UserRepository repository, AuthenticationSettings auth) : IRequestHandler<IRequestOutput, IRequestInput>
    {
        public IRequestOutput? Handle(IRequestInput input)
        {
            var command = input as AuthenticateCommand;
            var user = repository.GetAll().FirstOrDefault(user => command.UserCredentials.Email == user.Email);

            if (user != null)
            {
                string salt = user.Salt;
                string password = command.UserCredentials.Password + salt;
                byte[] encodedPassword = Encoding.UTF8.GetBytes(password);
                byte[] passwordHash = SHA256.HashData(encodedPassword);

                if (Convert.ToBase64String(passwordHash) == user.Password)
                    return new AuthenticateResponse(token: GenerateToken(user));
                else
                    return new AuthenticateResponse(message: "incorrect password");

            }
            return new AuthenticateResponse(message: "incorrect email");
        }
        private string GenerateToken(User user)
        {
            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(auth.PrivateKey);

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
            claimsIdentity.AddClaim(new Claim(type: ClaimTypes.NameIdentifier, value: user.Id.ToString()));

            return claimsIdentity;
        }
    }
}
