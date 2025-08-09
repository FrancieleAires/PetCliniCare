using CliniCare.Application.Commands.Animals.DeleteAnimal;
using CliniCare.Application.Commands.Animals.InsertAnimal;
using CliniCare.Application.Commands.Animals.UpdateAnimal;
using CliniCare.Application.Queries.Animal;
using CliniCare.Application.Queries.Animal.GetAllAnimals;
using CliniCare.Application.Queries.Animal.GetIdAnimal;
using CliniCare.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CliniCare.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/animals")]
    public class AnimalController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AnimalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost()]
        public async Task<IActionResult> CreateAnimalByClientAsync([FromBody] InsertAnimalCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok("Animal cadastrado com sucesso.");
            }

            return BadRequest(new { Message = "Não foi possível criar seu pet." });
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateAnimalByClientAsync([FromBody] UpdateAnimalCommand command)
        {
            var result = await _mediator.Send(command);

            if(result.IsSuccess)
            {
                return Ok("Animal atualizado com sucesso");
            }
            return BadRequest(new { Message = "Não foi possível atualizar seu pet." });
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllAnimalsByClientIdAsync()
        {
            var query = new GetAllAnimalsByClientQuery(); 
            var result = await _mediator.Send(query);

            if (result.IsSuccess)
            {
                return Ok(result.Value); 
            }

            return BadRequest(new { Message = "Não foi possível ver seus pets!" }); 
        }
        [HttpGet("{animalId}")]
        public async Task<IActionResult> GetAnimalByIdAsync(int animalId)
        {
            var query = new GetAnimalByIdQuery(animalId);
            var result = await _mediator.Send(query);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(new { Message = "Não foi possível ver seu pet!" });
        }
        [HttpDelete("{animalId}")]
        public async Task<IActionResult> DeleteAnimalAsync(int animalId)
        {
            var query = new DeleteAnimalCommand(animalId);
            var result = await _mediator.Send(query);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(new { Message = "Não foi possível deletar seu pet!" });
        }

    }
}
