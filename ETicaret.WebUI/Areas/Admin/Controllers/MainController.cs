using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.WebUI.Areas.Admin.Controllers
{
    public class MainController : Controller
    {
        [Area("Admin"), Authorize(Policy = "AdminPolicy")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
