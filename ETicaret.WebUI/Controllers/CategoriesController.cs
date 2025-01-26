using ETicaret.Core.Entities;
using ETicaret.Data;
using ETicaret.Service.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.WebUI.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IService<Category> _appCategoryService;

        public CategoriesController(IService<Category> appCategoryService)
        {
            _appCategoryService = appCategoryService;
        }


        public async Task<IActionResult> IndexAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _appCategoryService.GetQueryable().Include(p => p.Products)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
    }
}