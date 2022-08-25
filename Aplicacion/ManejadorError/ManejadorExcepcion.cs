using System.Net;
namespace Aplicacion.ManejadorError
{
    public class ManejadorExcepcion : Exception
    {
        public HttpStatusCode Codigo  {get;}
        public string Message{get;}
        public ManejadorExcepcion(HttpStatusCode codigo, string message) {
            Codigo = codigo;
            Message = message;
        }
    }
}