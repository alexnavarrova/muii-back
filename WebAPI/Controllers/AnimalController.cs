using Aplicacion.Animales;
using Aplicacion.Animales.dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : MiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<AnimalDto>>> Get()
        {
            return await Mediator.Send(new Consulta.ListQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AnimalDto>> GetById(Guid id)
        {
            return await Mediator.Send(new Get.GetByIdQuery { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Crear.AnimalCommand data)
        {
            return await Mediator.Send(data);
        }

        [HttpPut]
        public async Task<ActionResult<Unit>> Modificar(Modificar.AnimalCommand data)
        {
            return await Mediator.Send(data);
        }

    }
}
