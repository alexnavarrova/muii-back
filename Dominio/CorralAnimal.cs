namespace Dominio
{
    public class CorralAnimal
    {
        public Guid AnimalId { get; set; }
        public Animal Animal { get; set; }
        public Guid CorralId { get; set; }
        public Corral Corral { get; set; }
    }
}
