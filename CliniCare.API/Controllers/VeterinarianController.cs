using CliniCare.Application.InputModels.Veterinarian;
using CliniCare.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace CliniCare.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/veterinarian")]
    public class VeterinarianController : ControllerBase
    {
        private readonly IVeterinarianService _veterinianService;

        public VeterinarianController(IVeterinarianService veterinianService)
        {
            _veterinianService = veterinianService;
        }


        [HttpGet("all-veterinarians")]
        public async Task<IActionResult> GetAllVeterinariansAsync()
        {
            var result = await _veterinianService.GetAllVeterinariansAsync();

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(new { Message = "Não foi possível consultar os veterinários." });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVeterinarianByIdAsync(int id)
        {
            var result = await _veterinianService.GetVeterinarianByIdAsync(id);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(new { Message = "Não foi possível consultar o veterinário pelo ID." });
            }
        }
        [HttpPut("update-veterinarian")]
        public async Task<IActionResult> UpdateVeterinarianAsync(int id, UpdateVeterinarianInputModel updateVeterinarianInputModel)
        {
            var result = await _veterinianService.UpdateVeterinarianAsync(id, updateVeterinarianInputModel);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(new { Message = "Não foi possível atualizar o veterinário." });
            }

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVeterinarianAsync(int id)
        {
            var result = await _veterinianService.DeleteVeterinarianAsync(id);
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }


    }
}
