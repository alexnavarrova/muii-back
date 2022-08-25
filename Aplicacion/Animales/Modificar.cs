using Aplicacion.Animales.dto;
using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;
using System.Net;

namespace Aplicacion.Animales
{
    public class Modificar
    {
        public class AnimalCommand : IRequest
        {
            public Guid Id { get; set; }
            public string Nombre { get; set; }
            public bool AltaPeligrosidad { get; set; }
            public DateTime FechaNacimiento { get; set; }
            public List<AnimalDto>? RestrincionAnimal { get; set; }
        }

        public class ModificarAnimalValidar : AbstractValidator<AnimalCommand>
        {
            public ModificarAnimalValidar()
            {
                RuleFor(x => x.Id).NotEmpty();
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.FechaNacimiento).NotEmpty();                
            }
        }

        public class ModificarAnimallManejador : IRequestHandler<AnimalCommand>
        {
            private readonly MuiiContext _context;
            public ModificarAnimallManejador(MuiiContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(AnimalCommand request, CancellationToken cancellationToken)
            {
                var animal = _context.Animal.SingleOrDefault(x => x.AnimalId == request.Id);

                if (animal == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, "No se encontro el animal");
                }

                animal.Nombre = request.Nombre;
                animal.AltaPeligrosidad = request.AltaPeligrosidad;
                animal.FechaNacimiento = request.FechaNacimiento;
                _context.Animal.Update(animal);

                var animalesNoConvive = _context.RestrincionAnimal.Where(x => x.AnimalId == request.Id).ToList();
                _context.RestrincionAnimal.RemoveRange(animalesNoConvive);

                if (request.RestrincionAnimal?.Count > 0)
                {
                    List<RestrincionAnimal> restrincionAnimal = new();
                    foreach (var ra in request.RestrincionAnimal)
                    {
                        var animalExiste = _context.Animal.SingleOrDefault(x => x.AnimalId == request.Id);
                        if (animalExiste == null) throw new ManejadorExcepcion(HttpStatusCode.NotFound, "No se encontro el animal");

                        restrincionAnimal.Add(new RestrincionAnimal
                        {
                            AnimalId = animal.AnimalId,
                            AnimalNoConviveId = ra.AnimalId
                        });
                    }
                    await _context.RestrincionAnimal.AddRangeAsync(restrincionAnimal);
                }

                var resultados = await _context.SaveChangesAsync();

                if (resultados > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo modificar el animal");
            }
        }
    }
}
