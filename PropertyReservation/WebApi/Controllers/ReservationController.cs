using Application.DTOs.Reservation;
using Application.Services;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationService _reservationService;

        public ReservationController(ReservationService reservationService)
        {
            _reservationService = reservationService;
        }


        // GET: api/Reservation/my-reservations
        [Authorize(Roles = "User")]
        [HttpGet("my-reservations")]
        public async Task<ActionResult<IEnumerable<MyReservationResponseDTO>>> GetMyReservations()
        {
            try
            {
                var reservations = await _reservationService.GetReservationsByUserIdAsync();
                return Ok(reservations);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Reservation/by-property/5
        [Authorize(Roles = "Owner")]
        [HttpGet("by-property/{propertyId}")]
        public async Task<ActionResult<IEnumerable<ReservationResponseDTO>>> GetReservationsByPropertyId(int propertyId)
        {
            try
            {
                var reservations = await _reservationService.GetReservationsByPropertyIdForOwnerIdAsync(propertyId);
                return Ok(reservations);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // GET: api/Reservation/owner
        [Authorize(Roles = "Owner")]
        [HttpGet("owner")]
        public async Task<ActionResult<IEnumerable<ReservationResponseDTO>>> GetReservationsForOwner()
        {
            try
            {
                var reservations = await _reservationService.GetReservationsForOwnerIdAsync();
                return Ok(reservations);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Reservation
        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<ActionResult<ReservationResponseDTO>> PostReservation(ReservationRequestDTO reservationDTO)
        {
            try
            {
                var resertvationResponse = await _reservationService.CreateReservationAsync(reservationDTO);
                return Ok(resertvationResponse);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }

        }

        [Authorize(Roles = "Owner")]
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> ChangeReservationStatus(int id, [FromBody] ChangeReservationStatusDTO dto)
        {
            try
            {
                await _reservationService.ChangeStatusAsync(
                    id,
                    dto
                );

                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Owner, User")]
        [HttpGet("{reservationId}")]
        public async Task<ReservationResponseDTO> getReservationById(int reservationId)
        {
            try
            {
                var reservation = await _reservationService.GetReservationByIdAsync(reservationId);
                return reservation;
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
        }

    }
}
