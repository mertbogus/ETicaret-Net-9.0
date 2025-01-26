using ETicaret.Core.Entities;
using ETicaret.Data;
using ETicaret.Service.Abstract;
using ETicaret.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IService<Product> _appProductService;

        public ProductsController(IService<Product> appProductService)
        {
            _appProductService = appProductService;
        }

        public async Task<IActionResult> Index(string search="")
        {
            var databaseContext = _appProductService.GetAllAsync(p => p.IsActive && p.Name.Contains(search));
            return View(await databaseContext);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _appProductService.GetQueryable()
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            var model = new ProductDetailViewModel()
            {
                Products = product,
                ProductsRelated= _appProductService.GetQueryable().Where(p => p.IsActive && p.CategoryId== product.CategoryId
                && p.Id==product.Id)
            };
            return View(model);
        }
    }
}
