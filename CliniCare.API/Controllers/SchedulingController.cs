using CliniCare.Application.Commands.Schedulings;
using CliniCare.Application.Helpers;
using CliniCare.Application.Queries.Schedulings;
using CliniCare.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CliniCare.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("scheduling")]
    public class SchedulingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SchedulingController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> CreateSchedulingAsync([FromBody] InsertSchedulingCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess) if (result.IsSuccess) return Created();
           
            return BadRequest(new { Message = "Não foi possível cadastrar seu agendamento!" });
        }

       
        [HttpPatch("{schedulingId}/confirm")]
        public async Task<IActionResult> ConfirmSchedulingAsync(int schedulingId)
        {
            var request = new ConfirmSchedulingCommand(schedulingId);
            var result = await _mediator.Send(request);

            if (result.IsSuccess) return Ok("Agendamento confirmado!");

            return BadRequest(new { Message = "Não foi possível confirmar seu agendamento!" });
        }
        
        [HttpPatch("{schedulingId}/finalize")]
        public async Task<IActionResult> FinalizeSchedulingAsync(int schedulingId)
        {
            var request = new FinalizeSchedulingCommand(schedulingId);
            var result = await _mediator.Send(request);

            if (result.IsSuccess) return Ok("Agendamento finalizado!");

            return BadRequest(new { Message = "Não foi possível finalizar seu agendamento!" });
        }
        
        [HttpPatch("{schedulingId}/cancel")]
        public async Task<IActionResult> CancelSchedulingAsync(int schedulingId)
        {
            var request = new CancelSchedulingCommand(schedulingId);
            var result = await _mediator.Send(request);

            if (result.IsSuccess) return Ok("Agendamento cancelado!");

            return BadRequest(new { Message = "Não foi possível cancelar seu agendamento!" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSchedulingsAsync()
        {
            var query = new GetAllSchedulingsQuery();
            var result = await _mediator.Send(query);
            if (result.IsSuccess) return Ok(result);

            return BadRequest(new { Message = "Não foi possível consultar seu agendamento!" });
        }

        [HttpGet("{userId}/by-client")]
        public async Task<IActionResult> GetSchedulingByUserIdAsync(int userId)
        {
            var query = new GetSchedulingsByUserQuery(userId);
            
            var result = await _mediator.Send(query);


            return result.Match(
                  x => Ok(x), 
                  p => Problem(p) 
            );

        }
        [HttpGet("{schedulingId}/by-id")]
        public async Task<IActionResult> GetSchedulingByIdAsync(int schedulingId)
        {
            var query = new GetSchedulingByIdQuery(schedulingId);

            var result = await _mediator.Send(query);

            return result.Match(
                  x => Ok(x),
                  p => Problem(p)
            );

        }

        [NonAction]
        public IActionResult Problem(List<Error> errors)
        {
            if (errors != null && errors.Any())
            {
                var firstError = errors.First();
                // Mapeia o código de status com base no tipo do erro
                var statusCode = (int)firstError.Type;

                // Se você quiser retornar todos os erros, basta juntar as mensagens
                return StatusCode(statusCode, string.Join(", ", errors.Select(e => e.Message)));
            }
            else
            {
                // Caso não haja erros, retorna um erro genérico
                return StatusCode(500, "Erro desconhecido");
            }
        }

    }
}
