using Microsoft.AspNetCore.Identity;

namespace Dominio.Usuarios
{
    public class Usuario : IdentityUser
    {
        public string NombreCompleto {get;set;}
    }
}