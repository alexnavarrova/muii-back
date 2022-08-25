namespace Dominio
{
    public class Animal
    {
        public Guid AnimalId { get; set; }
        public string Nombre { get; set; }
        public bool AltaPeligrosidad { get; set; }
        public DateTimeOffset FechaNacimiento { get; set; }
        public DateTimeOffset? FechaCreacion { get; set; }
        public ICollection<RestrincionAnimal> RestrincionLink { get; set; }
    }
}
