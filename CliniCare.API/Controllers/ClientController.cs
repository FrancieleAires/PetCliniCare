using CliniCare.Application.InputModels.Client;
using CliniCare.Application.Services.Interfaces;
using CliniCare.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CliniCare.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("clients")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet("all-clients")]
        public async Task<IActionResult> GetAllClientsAsync()
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var result = await _clientService.GetAllClientsAsync();

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(new { Message = "Não há clientes para consultar." });
            }

        }

        [Authorize(Roles = "Client")]
        [HttpGet("profile-client")]
        public async Task<IActionResult> GetProfileClient()
        {
            var result = await _clientService.GetProfileClientAsync();
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(new { Message = "Não foi possível consultar seu perfil." });
            }
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientByIdAsync(int id)
        {
            var result = await _clientService.GetClientByIdAsync(id);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(new { Message = "Não foi possível encontrar o cliente com o ID fornecido." });
            }
        }
        [Authorize(Roles = "Client")]
        [HttpPut("update-client")]
        public async Task<IActionResult> UpdateClientAsync(UpdateClientInputModel updateClientInputModel)
        {
            var result = await _clientService.UpdateClientAsync(updateClientInputModel);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(new { Message = "Não foi possível atualizar os dados." });
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("update-by-admin")]
        public async Task<IActionResult> UpdateClientByAdminAsync(int clientId, UpdateClientInputModel updateClientInputModel)
        {
            var result = await _clientService.UpdateClientByAdminAsync(clientId, updateClientInputModel);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(new { Message = "Não foi possível atualizar pelo ID do cliente." });
            }
        }
    }
}
