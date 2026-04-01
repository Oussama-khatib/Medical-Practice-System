using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using App.core;
using Microsoft.IdentityModel.Tokens;

namespace WebApp.API.Service
{
    public static class TokenService
    {
        private static string SecretKey = "this_is_a_very_secret_key_change_it"; // same as in Program.cs

        public static string GenerateTokenUser(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey);

            // Claims = user data embedded in the token
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Email, user.Email ?? ""),
            //new Claim(ClaimTypes.Role, user.UserType ?? "user")
        };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(4), // valid for 4 hours
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static string GenerateTokenPatient(Patient patient)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey);

            // Claims = user data embedded in the token
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, patient.PatientId.ToString()),
            new Claim(ClaimTypes.Email, patient.Email ?? ""),
        };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(4), // valid for 4 hours
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
