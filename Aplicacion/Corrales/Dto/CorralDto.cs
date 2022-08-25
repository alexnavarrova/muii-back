using Aplicacion.Animales.dto;

namespace Aplicacion.Corrales.Dto
{
    public class CorralDto
    {
        public Guid CorralId { get; set; }
        public string Nombre { get; set; }
        public int Limite { get; set; }
        public List<AnimalDto> Animales { get; set; }

    }
}
