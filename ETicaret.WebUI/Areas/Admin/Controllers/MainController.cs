using Microsoft.AspNetCore.Mvc;

namespace ETicaret.WebUI.Areas.Admin.Controllers
{
    public class MainController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
