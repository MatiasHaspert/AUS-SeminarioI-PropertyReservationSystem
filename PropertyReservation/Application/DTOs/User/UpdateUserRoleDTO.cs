using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.DTOs.User
{
    public class UpdateUserRoleDTO
    {
        [Required]
        public Role Role { get; set; }
    }
}
