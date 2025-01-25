using System.Diagnostics;
using ETicaret.Core.Entities;
using ETicaret.Data;
using ETicaret.WebUI.Models;
using ETicaret.WebUI.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext _context;

        public HomeController(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomePageViewModel()
            {
                Sliders = await _context.Sliders.ToListAsync(),
                Productss = await _context.Products.Where(p => p.IsActive && p.IsHome).ToListAsync(),
                News = await _context.News.ToListAsync()
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        [Route("AccesDenied")]
        public IActionResult AccesDenied()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ContactUsAsync(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Contacts.AddAsync(contact);
                    var sonuc = await _context.SaveChangesAsync();
                    if (sonuc>0)
                    {
                        TempData["Message"] = @"<div class=""alert alert-succes alert-dismissible fade show"" role=""alert"">
                           <strong>Mesajýnýz Gönderilmiþtir.</strong> 
                           <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
                           </div>";
                       /*wait MailHelper.SendMailAsync(contact);*/
                        return RedirectToAction("ContactUs");
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Gönderirken Bir Hata Oluþtu.");
                    throw;
                }
            }

            return View(contact);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
