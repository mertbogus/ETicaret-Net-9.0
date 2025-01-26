using ETicaret.Core.Entities;
using ETicaret.Service.Abstract;

namespace ETicaret.Service.Concrete
{
    public class CardService : ICardService
    {
        public List<CardLine> CardLines = new();
        public void AddProduct(Product product, int quantity)
        {
            var products = CardLines.FirstOrDefault(p => p.Product.Id == quantity);
            if (products != null)
            {
                products.Quantity += quantity;

            }
            else
            {
                CardLines.Add(new CardLine
                {
                    Quantity = quantity,
                    Product = product
                });
            }
        }

        public void ClearAll()
        {
            CardLines.Clear();
        }

        public void RemoveProduct(Product product)
        {
            CardLines.RemoveAll(p=>p.Product.Id==product.Id);
        }

        public decimal TotalPrice()
        {
            return CardLines.Sum(p=>p.Product.Price*p.Quantity);
        }

        public void UpdateProduct(Product product, int quantity)
        {
            var products = CardLines.FirstOrDefault(p => p.Product.Id == quantity);
            if (products != null)
            {
                products.Quantity = quantity;

            }
            else
            {
                CardLines.Add(new CardLine
                {
                    Quantity = quantity,
                    Product = product
                });
            }
        }
    }
}
