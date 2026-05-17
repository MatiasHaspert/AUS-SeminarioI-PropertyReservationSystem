using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.ValueObjects;
using Domain.Enums;

namespace Domain.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        
        public string LastName { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        
        public Address? Address { get; set; }
        
        [MaxLength(20)]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public Role Role { get; set; } = Role.User;

        // Navigation properties
        // public ICollection<Property> Properties { get; set; } = new List<Property>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
