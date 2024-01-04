using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ReservaFacil.Dominio.Modelos;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReservaFacil.Dominio.Servicos.Servicos
{
    public static class TokenService
    {
        public static string GerarToken(Usuario usuario, IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new InvalidOperationException("O IConfiguration não inicializou.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
