using Dominio;

namespace Aplicacion.Animales.dto
{
    public class AnimalDto
    {
        public Guid AnimalId { get; set; }
        public string? Nombre { get; set; }
        public bool AltaPeligrosidad { get; set; }
        public DateTimeOffset FechaNacimiento { get; set; }
        public List<Animal>? AnimalNoConvive { get; set; }

        public string Edad
        {
            get {
                decimal diferencia = (DateTimeOffset.UtcNow - FechaNacimiento).Days / 365;
                var anios = Math.Ceiling(diferencia);
                return anios +  (anios > 1 ? " años" : " año" ); 
            }
        }


    }
}
