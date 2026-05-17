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

        // Endpoint publico para obtener todas las propiedades
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PropertyListResponseDTO>>> GetProperties()
        {
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

        // Endpoint protegido para que un owner pueda actualizar una propiedad suya por su id
        [HttpPut("{id}")]
        [Authorize(Roles = "Owner")]
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

        [HttpDelete("{propertyId}")]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> DeleteSafeProperty(int propertyId)
        {
            try
            {
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

    }
}
