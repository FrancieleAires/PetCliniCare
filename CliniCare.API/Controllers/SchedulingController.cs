using CliniCare.Application.Commands.Schedulings;
using CliniCare.Application.Commands.Schedulings.ConfirmScheduling;
using CliniCare.Application.Commands.Schedulings.FinalizeScheduling;
using CliniCare.Application.Commands.Schedulings.InsertSchedulingClient;
using CliniCare.Application.Commands.Schedulings.InsertSchedulingClinic;
using CliniCare.Application.Helpers;
using CliniCare.Application.Queries.Schedulings;
using CliniCare.Application.Queries.Schedulings.GetAllScheduling;
using CliniCare.Application.Queries.Schedulings.GetIdScheduling;
using CliniCare.Application.Queries.Schedulings.GetUserIdScheduling;
using CliniCare.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CliniCare.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/scheduling")]
    public class SchedulingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SchedulingController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("client")]
        public async Task<IActionResult> CreateSchedulingByClientAsync([FromBody] InsertSchedulingClientCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess) if (result.IsSuccess) return Created();
           
            return BadRequest(result.Errors);
        }
        [HttpPost("clinic")]
        public async Task<IActionResult> CreateSchedulingAsync([FromBody] InsertSchedulingClinicCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess) if (result.IsSuccess) return Created();

            return BadRequest(result.Errors);
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllSchedulingsAsync()
        {
            var query = new GetAllSchedulingsQuery();
            var result = await _mediator.Send(query);
            if (result.IsSuccess) return Ok(result);

            return BadRequest(new { Message = "Não foi possível consultar seu agendamento!" });
        }

        [HttpGet("by-client")]
        public async Task<IActionResult> GetSchedulingByUserIdAsync()
        {
            var query = new GetAllSchedulingsByUserQuery();
            
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
                var statusCode = (int)firstError.Type;

                return StatusCode(statusCode, string.Join(", ", errors.Select(e => e.Message)));
            }
            else
            {
               
                return StatusCode(500, "Erro desconhecido");
            }
        }

    }
}
