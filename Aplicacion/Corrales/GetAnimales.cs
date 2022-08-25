using Aplicacion.Animales.dto;
using Aplicacion.Corrales.Dto;
using Aplicacion.ManejadorError;
using AutoMapper;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System.Net;

namespace Aplicacion.Corrales
{
    public class GetAnimales
    {
        public class ByGranjaIdQuery : IRequest<List<AnimalDto>> {
            public Guid Id { get; set; }
        }

        public class Manejador : IRequestHandler<ByGranjaIdQuery, List<AnimalDto>>
        {
            private readonly MuiiContext _context;
            private readonly IMapper _mapper;
            public Manejador(MuiiContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<AnimalDto>> Handle(ByGranjaIdQuery request, CancellationToken cancellationToken)
            {
                var corral = _context.Corral
                    .Include(x => x.AnimalLink)
                    .ThenInclude(x => x.Animal)
                    .SingleOrDefault(x => x.CorralId == request.Id);

                if (corral == null) throw new ManejadorExcepcion(HttpStatusCode.NotFound, "No se encontro el corral");
                var animales = corral.AnimalLink.Select(x => x.Animal)?.ToList();
                return _mapper.Map<List<Animal>, List<AnimalDto>>(animales);
            }
        }

    }
}
