using Application.DTOs.Admin;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    // Dashboard del Administrador.
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AdminStatsService _adminStatsService;

        public AdminController(AdminStatsService adminStatsService)
        {
            _adminStatsService = adminStatsService;
        }

        // GET: api/admin/stats
        [HttpGet("stats")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<AdminStatsDTO>> GetStats()
        {
            var stats = await _adminStatsService.GetStatsAsync();
            return Ok(stats);
        }
    }
}
