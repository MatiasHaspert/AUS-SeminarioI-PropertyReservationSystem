using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Application.DTOs.PropertyAvailability;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyAvailabilityController : ControllerBase
    {
        private readonly PropertyAvailabilityService _propertyAvailabilityService;

        public PropertyAvailabilityController(PropertyAvailabilityService propertyAvailabilityService)
        {
            _propertyAvailabilityService = propertyAvailabilityService;
        }

        // GET: api/PropertyAvailability/5
        // Endpoint publico para obtener disponibilidades de una propiedad
        [AllowAnonymous]
        [HttpGet("{propertyId}")]
        public async Task<ActionResult<IEnumerable<PropertyAvailabilityResponseDTO>>> GetPropertyAvailabilities(int propertyId)
        {
            try
            {
                return Ok(await _propertyAvailabilityService.GetPropertyAvailabilitiesAsync(propertyId));
            }
            catch(ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT: api/PropertyAvailability/5
        // Endpoint protegido para actualizar disponibilidades de una propiedad
        [HttpPut("{availabilityId}")]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> PutPropertyAvailability(int availabilityId, PropertyAvailabilityRequestDTO propertyAvailabilityDTO)
        {
            try
            {
                await _propertyAvailabilityService.UpdatePropertyAvailabilityAsync(availabilityId, propertyAvailabilityDTO);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            } catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            } catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
        }

        // POST: api/PropertyAvailability
        // Endpoint protegido para crear disponibilidades de una propiedad
        [HttpPost]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult<PropertyAvailabilityResponseDTO>> PostPropertyAvailability(PropertyAvailabilityRequestDTO propertyAvailabilityDTO)
        {
            try
            {
                var createdAvailability = await _propertyAvailabilityService.CreatePropertyAvailabilityAsync(propertyAvailabilityDTO);
                return CreatedAtAction("GetPropertyAvailabilities", new { propertyId = createdAvailability.PropertyId }, createdAvailability);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }

        }
        
        // DELETE: api/PropertyAvailability/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> DeletePropertyAvailability(int id)
        {
            try
            {
                await _propertyAvailabilityService.DeletePropertyAvailabilityAsync(id);
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
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
        }
    }
}
