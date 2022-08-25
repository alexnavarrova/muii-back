using Aplicacion.Animales.dto;
using Aplicacion.Corrales;
using Aplicacion.Corrales.Dto;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CorralController : MiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<CorralDto>>> Get()
        {
            return await Mediator.Send(new Consulta.ListaQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CorralDto>> GetById(Guid id)
        {
            return await Mediator.Send(new  GetById.GetByIdQuery { Id = id });
        }

        [HttpGet("{id}/animales")]
        public async Task<ActionResult<List<AnimalDto>>> GetAnimalesByGranjaId(Guid id)
        {
            return await Mediator.Send(new GetAnimales.ByGranjaIdQuery { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Crear.CorralCommand data)
        {
            return await Mediator.Send(data);
        }

        [HttpPut]
        public async Task<ActionResult<Unit>> Modificar(Modificar.CorralCommand data)
        {
            return await Mediator.Send(data);
        }

        [HttpPost("agregar-animal")]
        public async Task<ActionResult<Unit>> AgregarAnimal(AgregarAnimal.CorralCommand data)
        {
            return await Mediator.Send(data);
        }
    }
}
