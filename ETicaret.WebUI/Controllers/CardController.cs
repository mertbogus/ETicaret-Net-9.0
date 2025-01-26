using ETicaret.Core.Entities;
using ETicaret.Service.Abstract;
using ETicaret.Service.Concrete;
using ETicaret.WebUI.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.WebUI.Controllers
{
    public class CardController : Controller
    {
        private readonly IService<Product> _appProductService;

        public CardController(IService<Product> appProductService)
        {
            _appProductService = appProductService;
        }

        public IActionResult Index()
        {
            return View();
        }

        private CardService GetCard()
        {
            return HttpContext.Session.GetJson<CardService>("Card") ?? new CardService();
        }

        public IActionResult Add(int productId, int quantity=1)
        {
            var product = _appProductService.Find(productId);
            if (product!=null)
            {
                var card = GetCard();
                card.AddProduct(product,quantity);
                HttpContext.Session.SetJson("Card",card);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Update(int productId, int quantity = 1)
        {
            var product = _appProductService.Find(productId);
            if (product != null)
            {
                var card = GetCard();
                card.UpdateProduct(product, quantity);
                HttpContext.Session.SetJson("Card", card);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int productId)
        {
            var product = _appProductService.Find(productId);
            if (product != null)
            {
                var card = GetCard();
                card.RemoveProduct(product);
                HttpContext.Session.SetJson("Card", card);
            }
            return RedirectToAction("Index");
        }
    }
}
