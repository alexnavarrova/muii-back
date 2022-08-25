using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio
{
    public class RestrincionAnimal
    {
        public Guid AnimalId { get; set; }
        
        [ForeignKey("AnimalId")]
        public Animal Animal { get; set; }

        public Guid AnimalNoConviveId { get; set; }

        [ForeignKey("AnimalNoConviveId")]
        public Animal AnimalNoConvive { get; set; }

    }
}
