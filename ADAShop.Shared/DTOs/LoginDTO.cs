using System.ComponentModel.DataAnnotations;

namespace ADAShop.Shared.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Usuario")]
        public string UserName { get; set; } = null!;

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MinLength(6, ErrorMessage = "El campo {0} debe tener al menos {1} carácteres.")]
        public string Password { get; set; } = null!;
    }
}