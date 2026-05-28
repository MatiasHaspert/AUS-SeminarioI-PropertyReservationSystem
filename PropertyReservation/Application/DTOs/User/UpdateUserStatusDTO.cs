using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.User
{
    public class UpdateUserStatusDTO
    {
        [Required]
        public bool IsActive { get; set; }
    }
}
