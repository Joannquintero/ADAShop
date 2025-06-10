using ADAShop.Shared.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADAShop.Web.ViewModels.Product
{
    public class ProductWithListOfCatesVM
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Descripción")]
        public string? Description { get; set; }
   
        public string ImageUrl { get; set; } = "wwwroot/images/sfdsdf";  // default image

        [NotMapped]
        [Display(Name = "Fotografía")]
        public IFormFile? image { get; set; }

        [Column(TypeName = "Money")]
        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal Price { get; set; } = 0;

        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public float Quantity { get; set; } = 0;

        [ForeignKey("Category")]
        [Display(Name = "Categoría")]
        public int CategoryId { get; set; }

        public List<Category>? categories { get; set; }
    }
}