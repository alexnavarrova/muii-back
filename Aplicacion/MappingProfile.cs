using Aplicacion.Animales.dto;
using Aplicacion.Corrales.Dto;
using AutoMapper;
using Dominio;

namespace Aplicacion
{
    public class MappingProfile : Profile
    {
        public MappingProfile(){
            CreateMap<Corral, CorralDto>()
            .ForMember(x => x.Animales, y => y.MapFrom(z => z.AnimalLink.Select(a => a.Animal).ToList()));

            CreateMap<Animal, AnimalDto>()
            .ForMember(x => x.AnimalNoConvive, y => y.MapFrom(z => z.RestrincionLink.Select(a => a.AnimalNoConvive).ToList()));
        }
    }
}