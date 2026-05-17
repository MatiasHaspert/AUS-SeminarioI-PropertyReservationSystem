using Application.DTOs.User;
using Application.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserResponseDTO>> Register([FromBody] UserRegisterDTO request)
        {
            try
            {
                var userResponseDTO = await _authService.RegisterAsync(request);
                return Ok(userResponseDTO);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDTO>> Login([FromBody] UserLoginDTO request)
        {
            try
            {
                var loginResponseDTO = await _authService.LoginAsync(request);
                return Ok(loginResponseDTO);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<UserResponseDTO>> GetCurrentUser()
        {
            try
            {
                var userResponseDTO = await _authService.GetCurrentUserAsync();
                return Ok(userResponseDTO);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }


    }
}
