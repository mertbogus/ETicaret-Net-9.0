using System.Diagnostics;
using ETicaret.Core.Entities;
using ETicaret.Data;
using ETicaret.Service.Abstract;
using ETicaret.WebUI.Models;
using ETicaret.WebUI.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IService<Product> _appProductService;
        private readonly IService<Slider> _appSliderService;
        private readonly IService<News> _appNewsService;
        private readonly IService<Contact> _appContactService;
        public HomeController(IService<Product> appProductService, IService<Slider> appSliderService, IService<News> appNewsService, IService<Contact> appContactService)
        {
            _appProductService = appProductService;
            _appSliderService = appSliderService;
            _appNewsService = appNewsService;
            _appContactService = appContactService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomePageViewModel()
            {
                Sliders = await _appSliderService.GetAllAsync(),
                Productss = await _appProductService.GetAllAsync(p => p.IsActive && p.IsHome),
                News = await _appNewsService.GetAllAsync()
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
                   await _appContactService.AddAsync(contact);
                    var sonuc = await _appContactService.SaveChangesAsync();
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
