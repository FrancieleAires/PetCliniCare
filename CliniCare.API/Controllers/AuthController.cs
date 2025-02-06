using CliniCare.Application.InputModels.Client;
using CliniCare.Application.Services.Interfaces;
using CliniCare.Application.Helpers;
using Microsoft.AspNetCore.Mvc;
using CliniCare.Application.InputModels.Veterinarian;
using CliniCare.Domain.Repositories;

namespace CliniCare.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class AuthController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IVeterinarianService _veterinarianService;

        public AuthController(IClientService clientService, IVeterinarianService veterinarianService)
        {
            _clientService = clientService;
            _veterinarianService = veterinarianService;
        }

        [HttpPost("register-client")]
        public async Task<IActionResult> CreateClientAsync([FromBody] CreateClientInputModel createClientInputModel)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var result = await _clientService.CreateClientAsync(createClientInputModel);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Errors);
            }

        }
        [HttpPost("login-client")]
        public async Task<IActionResult> LoginClientAsync([FromBody] LoginClientInputModel loginClientInputModel)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var result = await _clientService.LoginClientAsync(loginClientInputModel);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
        [HttpPost("register-veterinarian")]
        public async Task<IActionResult> CreateVeterinarianAsync([FromBody] CreateVeterinarianInputModel createVeterinarianInputModel)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var result = await _veterinarianService.CreateVeterinarianAsync(createVeterinarianInputModel);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Errors);
            }

        }
        [HttpPost("login-veterinarian")]
        public async Task<IActionResult> LoginVeterinarianAsync([FromBody] LoginVeterinarianInputModel loginVeterinarianInputModel)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var result = await _veterinarianService.LoginVeterinarianAsync(loginVeterinarianInputModel);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
    }
}

