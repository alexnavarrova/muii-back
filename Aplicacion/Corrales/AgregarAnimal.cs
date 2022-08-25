using Aplicacion.Animales.dto;
using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;
using System.Net;

namespace Aplicacion.Corrales
{
    public class AgregarAnimal
    {
        public class CorralCommand : IRequest
        {
            public Guid Id { get; set; }
            public List<AnimalDto>? Animales { get; set; }
        }

        public class AgregarAnimalCorralValidar : AbstractValidator<CorralCommand>
        {
            public AgregarAnimalCorralValidar()
            {
                RuleFor(x => x.Id).NotEmpty();
                RuleFor(x => x.Animales).NotEmpty();
            }
        }

        public class AgregarAnimalManejador : IRequestHandler<CorralCommand>
        {
            private readonly MuiiContext _context;
            public AgregarAnimalManejador(MuiiContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(CorralCommand request, CancellationToken cancellationToken)
            {

                var corral = _context.Corral.SingleOrDefault(x => x.CorralId == request.Id);
                if (corral == null) throw new ManejadorExcepcion(HttpStatusCode.NotFound, "No se encontro el corral");

                var actualesAnimalesEnCorral = _context.CorralAnimal.Where(x => x.CorralId == request.Id).ToList();

                List<CorralAnimal> corralesAnimal = new();

                if (actualesAnimalesEnCorral.Count + request.Animales.Count > corral.Limite)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, "La cantidad de animales para agregar superan el limite" );
                }

                foreach (var animal in request.Animales)
                {
                    var animalExiste = _context.Animal.SingleOrDefault(x => x.AnimalId == animal.AnimalId);
                    if (animalExiste == null) throw new ManejadorExcepcion(HttpStatusCode.NotFound, "El animal seleccionado no existe no existe");

                    if (actualesAnimalesEnCorral.SingleOrDefault(x => x.AnimalId == animal.AnimalId) != null)
                    {
                        throw new ManejadorExcepcion(HttpStatusCode.NotFound, "El animal seleccionado ya esta registrado en el corral");
                    }

                    corralesAnimal.Add(new CorralAnimal
                    {
                        CorralId = corral.CorralId,
                        AnimalId = animal.AnimalId,
                    });
                }
                _context.CorralAnimal.AddRange(corralesAnimal);

                var resultados = await _context.SaveChangesAsync();
                if (resultados > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo agregar los animales en el corral");
            }
        }
    }
}
