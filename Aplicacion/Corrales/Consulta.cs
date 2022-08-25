using Aplicacion.Corrales.Dto;
using AutoMapper;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Corrales
{
    public class Consulta
    {
        public class ListaQuery : IRequest<List<CorralDto>> { }

        public class Manejador : IRequestHandler<ListaQuery, List<CorralDto>>
        {
            private readonly MuiiContext _context;
            private readonly IMapper _mapper;
            public Manejador(MuiiContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<CorralDto>> Handle(ListaQuery request, CancellationToken cancellationToken)
            {
                var corrales = await _context.Corral.ToListAsync();
                return _mapper.Map<List<Corral>, List<CorralDto>>(corrales);
            }
        }

    }
}
