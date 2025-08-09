using CliniCare.Application.Queries.DashboardClient;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CliniCare.API.Controllers
{
    [ApiController]
    [Authorize(Roles = "Client")]
    [Route("api/dashboard-client")]
    public class DashboardClientController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DashboardClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet()]
        public async Task<IActionResult> GetDashboardClientAsync()
        {
            var query = new DashboardClientQuery();
            var result = await _mediator.Send(query);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(new { Message = "Não foi possível obter os dados do dashboard." });
        }
    }
}
