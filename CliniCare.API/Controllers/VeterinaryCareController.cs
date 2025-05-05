using CliniCare.Application.Commands.VetCares.DeleteVetCare;
using CliniCare.Application.Commands.VetCares.InsertVetCare;
using CliniCare.Application.Commands.VetCares.UpdateVetCare;
using CliniCare.Application.Queries.VeterinaryCare;
using CliniCare.Application.Queries.VeterinaryCare.GetAllVetCare;
using CliniCare.Application.Queries.VeterinaryCare.GetIdVetCare;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CliniCare.API.Controllers
{
    [Authorize]
    [Route("api/vet-care")]
    [ApiController]
    public class VeterinaryCareController : ControllerBase
    {
        private IMediator _mediator;

        public VeterinaryCareController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateVeterinaryCareAsync([FromBody] InsertVetCareCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
            {
                return Ok("Tratamento cadastrado com sucesso.");
            }

            return BadRequest(new { Message = "Não foi possível criar seu tratamento." });
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateVeterinaryCareAsync([FromBody] UpdateVetCareCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
            {
                return Ok("Tratamento atualizado com sucesso.");
            }

            return BadRequest(new { Message = "Não foi possível atualizar seu tratamento." });
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVeterinaryCareAsync(int id)
        {
            var query = new DeleteVetCareCommand(id);
            var result = await _mediator.Send(query);
            if (result.IsSuccess)
            {
                return Ok("Tratamento deletado com sucesso.");
            }

            return BadRequest(new { Message = "Não foi possível deletar seu tratamento." });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVeterinaryCareAsync()
        {
            var query = new GetAllVetCaresQuery();
            var result = await _mediator.Send(query);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(new { Message = "Não foi possível consultar seus tratamentos." });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVeterinaryCareByIdAsync(int id)
        {
            var query = new GetVetCareByIdQuery(id);
            var result = await _mediator.Send(query);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(new { Message = "Não foi possível consultar seu tratamento." });

        }
    }
}
    

