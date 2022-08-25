namespace Dominio
{
    public class Corral
    {
        public Guid CorralId { get; set; }
        public string Nombre { get; set; }
        public int Limite { get; set; }
        public DateTime? FechaCreacion { get; set; }

        public ICollection<CorralAnimal> AnimalLink { get; set; }
    }
}
