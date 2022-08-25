using Aplicacion.Animales.dto;
using Dominio;
using Dominio.DateTimeColombia;
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.Animales
{
    public class Crear
    {
        public class AnimalCommand : IRequest
        {
            public string Nombre { get; set; }
            public bool AltaPeligrosidad { get; set; }
            public DateTime FechaNacimiento { get; set; }            
            public List<AnimalDto>? RestrincionAnimal { get; set; }
        }

        public class CrearAnimalValidar : AbstractValidator<AnimalCommand>
        {
            public CrearAnimalValidar()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.FechaNacimiento).NotEmpty();
            }
        }

        public class CrearAnimallManejador : IRequestHandler<AnimalCommand>
        {
            private readonly MuiiContext _context;
            public CrearAnimallManejador(MuiiContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(AnimalCommand request, CancellationToken cancellationToken)
            {
                var animal = new Animal
                {
                    Nombre = request.Nombre,
                    FechaNacimiento = DateTimeColombia.GetDateTimeOffset(request.FechaNacimiento),
                    AltaPeligrosidad = request.AltaPeligrosidad
                };

                _context.Animal.Add(animal);

                var resultados = await _context.SaveChangesAsync();

                if (resultados > 0)
                {
                    if (request.RestrincionAnimal != null &&  request.RestrincionAnimal.Count > 0)
                    {
                        List<RestrincionAnimal> restrincionAnimal = new();
                        foreach (var ra in request.RestrincionAnimal)
                        {
                            restrincionAnimal.Add(new RestrincionAnimal
                            {
                                AnimalId = animal.AnimalId,
                                AnimalNoConviveId = ra.AnimalId
                            });
                        }
                        await _context.RestrincionAnimal.AddRangeAsync(restrincionAnimal);
                    }
                    

                    return Unit.Value;
                }
                throw new Exception("No se pudo crear el animal");
            }
        }
    }
}
