using DataAccess.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Utilities
{
    public class TokenManagement
    {
        private string SecretKey { get; set; } = "EstaEsLaClaveSecreta";

        private User user { get; set; } = new User(); 

        public string GenerateJWTToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

            var sign = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(

                issuer: null,
                audience: null,
                expires: DateTime.UtcNow.AddDays(1),

                claims: new[] {new Claim("User", user.User_Email),
                                new Claim("Role", user.User_Role.ToString())
                },

                signingCredentials: sign
                );
            token.SigningKey = key;

            var tokenHandler = new JwtSecurityTokenHandler();
            
            return tokenHandler.WriteToken(token);
        }
        
        public User TokenCredentialsGetter(string tokenJWT)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(tokenJWT) as JwtSecurityToken;

            var userEmail = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "User").Value;
            var userRole = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "Role" ).Value.ToString();

            user.User_Email = userEmail;
            user.User_Role = Convert.ToInt16(userRole);

            return user;
        }
    }
}
