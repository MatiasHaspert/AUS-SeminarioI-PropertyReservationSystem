using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application.DTOs.Review;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewService _reviewService;

        public ReviewController(ReviewService ReviewService)
        {
            _reviewService = ReviewService;
        }

        // GET: api/Review?propertyId=5
        // Endpoint público para obtener todas las reseñas de una propiedad específica
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewResponseDTO>>> GetPropertyReviews([FromQuery] int propertyId)
        {
            try
            {
                var reviews = await _reviewService.GetPropertyReviewsAsync(propertyId);
                return Ok(reviews);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: api/Review/5
        // Endpoint público para obtener una reseña específica por su ID
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewResponseDTO>> GetReview(int id)
        {
            try
            {
                var review = await _reviewService.GetReviewByIdAsync(id);
                return Ok(review);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        // PUT: api/Review/5
        // Endpoint protegido para usuarios autenticados donde pueden actualizar sus reseñas
        [HttpPut("{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> PutReview(int id, ReviewRequestDTO reviewRequestDTO)
        {
            try
            {
                await _reviewService.UpdateReviewAsync(id, reviewRequestDTO);
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
        }

        // MAQUETA — CU-07: el Administrador puede eliminar una reseña inapropiada.
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            try
            {
                await _reviewService.DeleteReviewAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/Review
        // Endpoint protegido para usuarios autenticados donde pueden crear nuevas reseñas
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<ReviewResponseDTO>> PostReview(ReviewRequestDTO reviewRequestDTO)
        {
            try
            {
                var createdReview = await _reviewService.CreateReviewAsync(reviewRequestDTO);
                return CreatedAtAction("GetReview", new { id = createdReview.Id, propertyId = createdReview.PropertyId }, createdReview);
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
