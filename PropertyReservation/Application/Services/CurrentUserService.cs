using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int UserId
        {
            get
            {
                var user = _httpContextAccessor.HttpContext?.User;

                if (user == null)
                {
                    throw new UnauthorizedAccessException("Contexto de usuario no disponible.");
                }

                var idClaim = user.FindFirst(ClaimTypes.NameIdentifier) ?? user.FindFirst("sub");

                if (idClaim == null || string.IsNullOrEmpty(idClaim.Value))
                {
                    throw new UnauthorizedAccessException("Token inválido: ID no encontrado.");
                }

                if (!int.TryParse(idClaim.Value, out int userId))
                {
                    throw new UnauthorizedAccessException("El ID del token no es un número válido.");
                }

                return userId;
            }
        }
    }
}
