﻿using ETicaret.Core.Entities;
using ETicaret.Service.Abstract;
using ETicaret.WebUI.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.WebUI.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly IService<Product> _appProductService;

        public FavoritesController(IService<Product> appProductService)
        {
            _appProductService = appProductService;
        }
        public IActionResult Index()
        {
            var values = GetFavorites();
            return View(values);
        }

        private List<Product> GetFavorites()
        {
            return HttpContext.Session.GetJson<List<Product>>("GetFavorites")??[];
        }

        public IActionResult Add(int productId)
        {
            var values= GetFavorites();
            var product = _appProductService.Find(productId);
            //bu id'ye ait ürün var mı yok mu ona bak ve favoriler bu product id'ye ait bir değer içermiyorsa
            if (product != null && !values.Any(p => p.Id == productId));
            {
                values.Add(product);
                HttpContext.Session.SetJson("GetFavorites", values);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int productId)
        {
            var values = GetFavorites();
            var product = _appProductService.Find(productId);
            //bu id'ye ait ürün var mı yok mu ona bak ve favoriler bu product id'ye ait bir değer içermiyorsa
            if (product != null && values.Any(p => p.Id == productId)) ;
            {
                values.RemoveAll(x=>x.Id==product.Id);
                HttpContext.Session.SetJson("GetFavorites", values);
            }
            return RedirectToAction("Index");
        }
    }
}
