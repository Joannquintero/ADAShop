using System.ComponentModel.DataAnnotations;

namespace ADAShop.Web.ViewModels.Account
{
    public class LoginMV
    {
        [Display(Name = "Usuario")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MinLength(3, ErrorMessage = "El campo {0} debe tener al menos {1} caractéres.")]
        public string UserName { get; set; } = null!;

        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "El campo {0} debe tener al menos {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Password { get; set; } = null!;

        [Display(Name = "Recuérdame")]
        public bool RememberMe { get; set; }
    }
}
