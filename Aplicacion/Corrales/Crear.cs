using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.Corrales
{
    public class Crear
    {
        public class CorralCommand : IRequest
        {
            public string Nombre { get; set; }
            public int Limite { get; set; }
        }

        public class CrearCorralValidar : AbstractValidator<CorralCommand>
        {
            public CrearCorralValidar()
            {
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
                var corral = new Corral
                {
                    Nombre = request.Nombre,
                    Limite = request.Limite
                };

                _context.Corral.Add(corral);

                var resultados = await _context.SaveChangesAsync();
                if (resultados > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo insertar el corral");
            }
        }
    }
}
