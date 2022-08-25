using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Aplicacion.Contratos;
using Dominio.Usuarios;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Seguridad
{
    public class JwtGenerador : IJwtGenerador
    {
        public string CrearToken(Usuario usuario, List<string> roles)
        {
            var claims = new List<Claim>{
                new Claim(JwtRegisteredClaimNames.NameId, usuario.UserName)
            };

            if(roles != null){
                foreach(var rol  in roles){
                    claims.Add(new Claim(ClaimTypes.Role, rol ));
                }
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("C7q3FBCJZq0b9RRH0Dq4lxWuBipEBkH6"));
            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescripcion = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credenciales
            };

            var tokenManejador = new JwtSecurityTokenHandler();
            var token = tokenManejador.CreateToken(tokenDescripcion);

            return tokenManejador.WriteToken(token);
        }
    }
}