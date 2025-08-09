using CliniCare.Application.Queries.DashboardClinic;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CliniCare.API.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("api/dashboard-clinic")]
    public class DashboardClinicController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DashboardClinicController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet] 
        public async Task<IActionResult> DashboardClinicAsync()
        {
            var query = new DashboardClinicQuery();
            var result = await _mediator.Send(query);
            if(result.IsSuccess)
            {
                return Ok(result.Value);
            }
            
            return BadRequest(result.Errors);
        }

    }
}
