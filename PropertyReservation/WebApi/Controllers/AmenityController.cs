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

        
    }
}
