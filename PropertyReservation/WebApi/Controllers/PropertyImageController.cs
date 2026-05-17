using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyImageController : ControllerBase
    {
        private readonly PropertyImageService _imageService;

        public PropertyImageController(PropertyImageService imageService)
        {
            _imageService = imageService;
        }

        // GET: api/PropertyImage/5
        // Endpoint publico para obtener imagenes de una propiedad
        [AllowAnonymous]
        [HttpGet("{propertyId}")]
        public async Task<IActionResult> GetImagesByProperty(int propertyId)
        {
            try
            {
                var images = await _imageService.GetImagesByPropertyAsync(propertyId);
                return Ok(images);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/PropertyImage/5
        // Endpoint protegido para subir imagenes a una propiedad
        [HttpPost("{propertyId}")]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> UploadImages(int propertyId, List<IFormFile> files)
        {
            try
            {
                var images = await _imageService.UploadPropertyImagesAsync(propertyId, files, Request);
                return Ok(images);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
        }

        // PUT: api/PropertyImage/5/main
        // Endpoint protegido para establecer una imagen como principal
        [HttpPut("main/{id}")]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> SetMainImage(int id)
        {
            try
            {
                var result = await _imageService.SetMainImageAsync(id);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
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

        // DELETE: api/PropertyImage/5
        // Endpoint protegido para eliminar una imagen
        [HttpDelete("{id}")]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            try
            {
                await _imageService.DeleteImageAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
        }

    }
}
