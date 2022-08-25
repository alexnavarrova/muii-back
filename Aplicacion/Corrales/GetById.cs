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
    public class GetById
    {
        public class GetByIdQuery : IRequest<CorralDto> {
            public Guid Id { get; set; }
        }

        public class Manejador : IRequestHandler<GetByIdQuery, CorralDto>
        {
            private readonly MuiiContext _context;
            private readonly IMapper _mapper;
            public Manejador(MuiiContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CorralDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
            {
                var corral = _context.Corral
                    .Include(x => x.AnimalLink)
                    .ThenInclude(x => x.Animal)
                    .SingleOrDefault(x => x.CorralId == request.Id);
                if (corral == null) throw new ManejadorExcepcion(HttpStatusCode.NotFound, "No se encontro el corral");
                return _mapper.Map<Corral, CorralDto>(corral);
            }
        }

    }
}
