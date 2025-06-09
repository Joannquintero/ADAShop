using ADAShop.Shared.Entities;

namespace ADAShop.Web.ViewModels.Home
{
    public class ProdCatCartVM
    {
        public List<Product>? Products { get; set; }

        public List<Category>? Categories { get; set; }

        public Cart? Cart { get; set; }
    }
}