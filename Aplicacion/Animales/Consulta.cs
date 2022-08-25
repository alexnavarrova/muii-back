using Aplicacion.Animales.dto;
using AutoMapper;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Animales
{
    public class Consulta
    {
        public class ListQuery : IRequest<List<AnimalDto>> { }

        public class Manejador : IRequestHandler<ListQuery, List<AnimalDto>>
        {
            private readonly MuiiContext _context;
            private readonly IMapper _mapper;

            public Manejador(MuiiContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<AnimalDto>> Handle(ListQuery request, CancellationToken cancellationToken)
            {
                var animales = await _context.Animal.ToListAsync();
                return _mapper.Map<List<Animal>, List<AnimalDto>>(animales);
            }
        }

    }
}
