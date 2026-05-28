using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs.Property;
using Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly PropertyService _PropertyService;

        public PropertyController(PropertyService PropertyService)
        {
            _PropertyService = PropertyService;
        }

        // Endpoint protegido para obtener las propiedades del propietario actual
        [HttpGet("my")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult<IEnumerable<PropertyListResponseDTO>>> GetMyProperties()
        {
            var properties = await _PropertyService.GetPropertiesByCurrentOwnerAsync();
            return Ok(properties);

        }

        // Endpoint publico para obtener todas las propiedades.
        // MAQUETA — CU-04: si llega ?includeDeleted=true y el usuario es Admin,
        // se deben devolver también las propiedades con IsDeleted=true.
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PropertyListResponseDTO>>> GetProperties(
            [FromQuery] bool includeDeleted = false)
        {
            if (includeDeleted)
            {
                if (!User.IsInRole("Admin"))
                {
                    return Forbid();
                }

                var all = await _PropertyService.GetAllPropertiesIncludingDeletedAsync();
                return Ok(all);
            }

            var properties = await _PropertyService.GetPropertiesAsync();
            return Ok(properties);
        }

        // Endpoint publico para obtener una propiedad con sus detalles
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyDetailsResponseDTO>> GetPropertyDetailsById(int id)
        {
            var property = await _PropertyService.GetPropertyByIdAsync(id);

            if (property == null)
            {
                return NotFound();
            }

            return property;
        }

        // Endpoint protegido para que un owner pueda actualizar una propiedad suya por su id.
        // CU-04: ampliado para que un Admin también pueda editar cualquier propiedad.
        [HttpPut("{id}")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<IActionResult> PutProperty(int id, [FromBody] PropertyRequestDTO property)
        {
            try
            {
                await _PropertyService.PutPropertyAsync(id, property);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
        }

        // POST: api/Property
        [HttpPost]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<ActionResult<PropertyListResponseDTO>> PostProperty([FromBody] PropertyRequestDTO propertyDTO)
        {
            try
            {
                PropertyListResponseDTO property = await _PropertyService.CreatePropertyAsync(propertyDTO);
                return CreatedAtAction(nameof(GetPropertyDetailsById), new { id = property.Id }, property);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Endpoint publico para buscar propiedades con filtros
        [AllowAnonymous]
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<PropertyListResponseDTO>>> SearchProperties([FromQuery] PropertySearchRequestDTO searchParams)
        {
            var properties = await _PropertyService.SearchPropertiesAsync(searchParams);
            return Ok(properties);
        }

        // CU-04: soft-delete (Owner o Admin) o hard-delete (solo Admin si ?hard=true).
        [HttpDelete("{propertyId}")]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<IActionResult> DeleteSafeProperty(int propertyId, [FromQuery] bool hard = false)
        {
            try
            {
                if (hard)
                {
                    if (!User.IsInRole("Admin"))
                    {
                        return Forbid();
                    }

                    await _PropertyService.HardDeletePropertyAsync(propertyId);
                    return NoContent();
                }

                await _PropertyService.DeleteSafePropertyAsync(propertyId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // MAQUETA — CU-04: restaurar una propiedad eliminada lógicamente (IsDeleted = true → false).
        [HttpPost("{propertyId}/restore")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> RestoreProperty(int propertyId)
        {
            try
            {
                await _PropertyService.RestorePropertyAsync(propertyId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
