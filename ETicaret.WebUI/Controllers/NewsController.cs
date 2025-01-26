using ETicaret.Core.Entities;
using ETicaret.Data;
using ETicaret.Service.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.WebUI.Controllers
{
    public class NewsController : Controller
    {

        private readonly IService<News> _appNewsService;

        public NewsController(IService<News> appNewsService)
        {
            _appNewsService = appNewsService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _appNewsService.GetAllAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _appNewsService.GetAsync(m => m.Id == id);
                
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }
    }
}