using System.ComponentModel.DataAnnotations;

namespace ADAShop.Web.ViewModels.Account
{
    public class RegisterVM
    {
        [Display(Name = "Usuario")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MinLength(3, ErrorMessage = "El campo {0} debe tener al menos {1} caractéres.")]
        public string UserName { get; set; } = null!;

        [Display(Name = "N. Documento")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string? Document { get; set; } = null!;

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MinLength(3, ErrorMessage = "El campo {0} debe tener al menos {1} caractéres.")]
        public string Name { get; set; } = null!;

        [Display(Name = "Apellido")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MinLength(3, ErrorMessage = "El campo {0} debe tener al menos {1} caractéres.")]
        public string LastName { get; set; } = null!;

        [Display(Name = "Dirección")]
        [MaxLength(200, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MinLength(3, ErrorMessage = "El campo {0} debe tener al menos {1} caractéres.")]
        public string Address { get; set; } = null!;

        [Display(Name = "Número de Teléfono")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string PhoneNumber { get; set; } = null!;

        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "El campo {0} debe tener al menos {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Password { get; set; } = null!;

        [Display(Name = "Confirmar Contraseña")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = null!;

        public bool IsUser { get; set; }
    }
}