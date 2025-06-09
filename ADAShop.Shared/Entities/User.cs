using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ADAShop.Shared.Entities
{
    public class User : IdentityUser<long>
    {
        [MaxLength(20)]
        [Required]
        public string Identification { get; set; } = null!;

        [MaxLength(50)]
        [Required]
        public string Name { get; set; } = null!;

        [MaxLength(50)]
        [Required]
        public string LastName { get; set; } = null!;

        [MaxLength(200)]
        [Required]
        public string Address { get; set; } = null!;

        public string FullName => $"{Name} {LastName}";
    }
}