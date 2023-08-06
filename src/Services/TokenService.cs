using CollegeHub.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CollegeHub.Services {
    public class TokenService {

        public static string GenerateToken(string Email, Guid Id, Role role, IConfiguration configuration) {

            Console.WriteLine(Id);

            var claims = new Claim[] {
                new Claim(ClaimTypes.Email, Email),
                new Claim(ClaimTypes.NameIdentifier, Id.ToString().ToUpper()),
                new Claim(ClaimTypes.Role, role.ToString())
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["JwtTokenSettings:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor {

                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(double.Parse(configuration["JwtTokenSettings:ExpiryTimeInHours"])),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = configuration["JwtTokenSettings:Audience"],
                Issuer = configuration["JwtTokenSettings:Issuer"],
            };


            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }

    }
}
