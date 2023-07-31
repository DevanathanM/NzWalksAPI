using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NZWalks.api.Repository
{
    public class SQLTokemRepository :ITokenRepository
    {
        private readonly IConfiguration configuration;

        public SQLTokemRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        
        public string CreateJWTToken(IdentityUser user, List<string> Roles)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            
            foreach (var role in Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, user.Email));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credential= new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token= new JwtSecurityToken(configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],claims,expires:DateTime.Now.AddMinutes(15),signingCredentials:credential);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        
    }
}
