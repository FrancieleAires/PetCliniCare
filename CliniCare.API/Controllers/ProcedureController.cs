using CliniCare.Application.Commands.Procedures.DeleteProcedure;
using CliniCare.Application.Commands.Procedures.InsertProcedure;
using CliniCare.Application.Commands.Procedures.UpdateProcedure;
using CliniCare.Application.Queries.Procedure;
using CliniCare.Application.Queries.Procedure.GetAllProcedure;
using CliniCare.Application.Queries.Procedure.GetIdProcedure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CliniCare.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/procedures")]
    public class ProcedureController : ControllerBase
    { 

        private IMediator _mediator;

        public ProcedureController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create-procedures")]
        public async Task<IActionResult> CreateProceduresAsync([FromBody] InsertProcedureCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess) return Ok("Procedimento criado com sucesso.");
            return BadRequest(new { Message = "Não foi possível criar este procedimento." });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-procedure")]
        public async Task<IActionResult> UpdateProcedureAsync([FromBody] UpdateProcedureCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess) return Ok("Procedimento atualizado com sucesso!.");
            return BadRequest(new { Message = "Não foi possível atualizar este procedimento." });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProcedureAsync(DeleteProcedureCommand command, int id)
        {
            command = new DeleteProcedureCommand(id);
            var result = await _mediator.Send(command);
            if (result.IsSuccess) return Ok("Procedimento deletado com sucesso.");
            return BadRequest(new { Message = "Não foi possível deletar este procedimento." });

        }
        [Authorize(Roles = "Admin")]
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllProceduresAsync()
        {
            var query = new GetAllProcedureQuery();
            var result = await _mediator.Send(query);
            if (result.IsSuccess) return Ok(result);

            return BadRequest(new { Message = "Não foi possível deletar este procedimento." });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProcedureByIdAsync(int id)
        {
            var query = new GetProcedureByIdQuery(id);
            var result = await _mediator.Send(query);
            if(result.IsSuccess) return Ok(result);
            return BadRequest(new { Message = "Não foi possível consultar este procedimento." });
        }



    }
}
