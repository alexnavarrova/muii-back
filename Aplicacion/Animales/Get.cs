using Aplicacion.Animales.dto;
using Aplicacion.ManejadorError;
using AutoMapper;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System.Net;

namespace Aplicacion.Animales
{
    public class Get
    {
        public class GetByIdQuery : IRequest<AnimalDto> {
            public Guid Id { get; set; }
        }

        public class Manejador : IRequestHandler<GetByIdQuery, AnimalDto>
        {
            private readonly MuiiContext _context;
            private readonly IMapper _mapper;
            public Manejador(MuiiContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<AnimalDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
            {
                var animal = _context.Animal
                    .Include(a => a.RestrincionLink)
                    .ThenInclude(c => c.AnimalNoConvive)
                    .SingleOrDefault(x => x.AnimalId == request.Id);

                if (animal == null) throw new ManejadorExcepcion(HttpStatusCode.NotFound, "No se encontro el animal");
                return _mapper.Map<Animal, AnimalDto>(animal);
            }
        }

    }
}
