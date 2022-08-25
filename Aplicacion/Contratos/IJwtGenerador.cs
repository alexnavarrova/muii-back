using System.Collections.Generic;
using Dominio.Usuarios;

namespace Aplicacion.Contratos
{
    public interface IJwtGenerador
    {
         string CrearToken(Usuario usuario, List<string> roles);
    }
}