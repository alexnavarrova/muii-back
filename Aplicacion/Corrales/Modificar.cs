using Aplicacion.Animales.dto;
using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;
using System.Net;

namespace Aplicacion.Corrales
{
    public class Modificar
    {
        public class CorralCommand : IRequest
        {
            public Guid Id { get; set; }
            public string Nombre { get; set; }
            public int Limite { get; set; }
            public List<AnimalDto>? Animales { get; set; }
        }

        public class CrearCorralValidar : AbstractValidator<CorralCommand>
        {
            public CrearCorralValidar()
            {
                RuleFor(x => x.Id).NotEmpty();
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Limite).NotEmpty().GreaterThan(0);
            }
        }

        public class CrearCorralManejador : IRequestHandler<CorralCommand>
        {
            private readonly MuiiContext _context;
            public CrearCorralManejador(MuiiContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(CorralCommand request, CancellationToken cancellationToken)
            {
                var corral = _context.Corral.SingleOrDefault(x => x.CorralId == request.Id);
                if (corral == null) throw new ManejadorExcepcion(HttpStatusCode.NotFound, "No se encontro el corral");

                corral.Nombre = request.Nombre;
                corral.Limite = request.Limite;
                _context.Corral.Update(corral);

                var actualesAnimalesEnCorral = _context.CorralAnimal.Where(x => x.CorralId == request.Id).ToList();

                if (actualesAnimalesEnCorral.Count + request?.Animales?.Count > corral.Limite)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, "La cantidad de animales para agregar superan el limite");
                }

                _context.CorralAnimal.RemoveRange(actualesAnimalesEnCorral);

                List<CorralAnimal> corralesAnimal = new();

                foreach (var animal in request.Animales)
                {
                    var animalExiste = _context.Animal.SingleOrDefault(x => x.AnimalId == animal.AnimalId);
                    if (animalExiste == null) throw new ManejadorExcepcion(HttpStatusCode.NotFound, "El animal seleccionado no existe no existe");

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
                throw new Exception("No se pudo modificar el corral");
            }
        }
    }
}
