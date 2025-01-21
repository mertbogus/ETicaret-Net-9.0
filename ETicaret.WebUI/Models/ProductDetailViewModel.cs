using ETicaret.Core.Entities;

namespace ETicaret.WebUI.Models
{
    public class ProductDetailViewModel
    {
        public Product? Products { get; set; }
        public IEnumerable<Product>? ProductsRelated { get; set; }
    }
}
