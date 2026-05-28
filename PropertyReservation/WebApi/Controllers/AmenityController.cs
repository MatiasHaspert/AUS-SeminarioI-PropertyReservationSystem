using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.DTOs.Amenity;
using Application.Services;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmenityController : ControllerBase
    {
        private readonly AmenityService _amenityService;

        public AmenityController(AmenityService amenityService)
        {
            _amenityService = amenityService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AmenityResponseDTO>>> GetAmenities()
        {
           return Ok(await _amenityService.GetAllAmenitiesAsync());
        }

        // MAQUETA — CU-08: alta de un nuevo amenity (Admin).
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<AmenityResponseDTO>> CreateAmenity([FromBody] AmenityRequestDTO dto)
        {
            try
            {
                var created = await _amenityService.CreateAmenityAsync(dto);
                return CreatedAtAction(nameof(GetAmenities), new { id = created.Id }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                // Nombre duplicado u otra violación de invariantes
                return Conflict(ex.Message);
            }
        }

        // MAQUETA — CU-08: edición del nombre de un amenity (Admin).
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UpdateAmenity(int id, [FromBody] AmenityRequestDTO dto)
        {
            try
            {
                await _amenityService.UpdateAmenityAsync(id, dto);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        // MAQUETA — CU-08: eliminación de un amenity (Admin).
        // Si está referenciado por propiedades, el servicio debe liberar la relación many-to-many.
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteAmenity(int id)
        {
            try
            {
                await _amenityService.DeleteAmenityAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
