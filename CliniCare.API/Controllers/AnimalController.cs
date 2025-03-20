using CliniCare.Application.Commands.Animals;
using CliniCare.Application.Queries.Animal;
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

        [HttpPost("register")]
        public async Task<IActionResult> CreateAnimalByClientAsync([FromBody] InsertAnimalCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok("Animal cadastrado com sucesso.");
            }

            return BadRequest(new { Message = "Não foi possível criar seu pet." });
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAnimalByClientAsync([FromBody] UpdateAnimalCommand command)
        {
            var result = await _mediator.Send(command);

            if(result.IsSuccess)
            {
                return Ok("Animal atualizado com sucesso");
            }
            return BadRequest(new { Message = "Não foi possível atualizar seu pet." });
        }

        [HttpGet("all-animals/{clientId}")]
        public async Task<IActionResult> GetAllAnimalsByClientIdAsync(int clientId)
        {
            var query = new GetAllAnimalsByClientQuery(clientId); 
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
