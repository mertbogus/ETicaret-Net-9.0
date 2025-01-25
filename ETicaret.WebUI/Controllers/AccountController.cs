using ETicaret.Core.Entities;
using ETicaret.Data;
using ETicaret.WebUI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ETicaret.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly DatabaseContext _context;

        public AccountController(DatabaseContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            AppUser user = _context.AppUsers.FirstOrDefault(x => x.UserGuid.ToString() == HttpContext.User.FindFirst("UserGuid").Value);
            if (user is null)
            {
                return NotFound();
            }
            var model = new UserEditViewModel()
            {
                Email= user.Email,
                Id= user.Id,
                Password= user.Password,
                Phone= user.Phone,
                Surname= user.Surname,
            };
            return View(model);
        }
        [HttpPost,Authorize]
        public IActionResult Index(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AppUser user = _context.AppUsers.FirstOrDefault(x => x.UserGuid.ToString() == HttpContext.User.FindFirst("UserGuid").Value);
                    if (user is not null)
                    {
                        user.Surname = model.Surname;
                        user.Phone= model.Phone;
                        user.Password= model.Password;
                        user.Email= model.Email;
                        _context.AppUsers.Update(user);
                        var sonuc=_context.SaveChanges();
                        if (sonuc > 0)
                        {
                            TempData["Message"] = @"<div class=""alert alert-succes alert-dismissible fade show"" role=""alert"">
                           <strong>Bilgileriniz Güncellenmiştir.</strong> 
                           <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
                           </div>";
                            /*wait MailHelper.SendMailAsync(contact);*/
                            return RedirectToAction("ContactUs");
                        }

                    }
                   

                }
                catch (Exception)
                {

                    ModelState.AddModelError("", "Bir Hata Oluştu");
                }
            }
            return View(model);
        }
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignInAsync(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                 try {
                    var account = await _context.AppUsers.FirstOrDefaultAsync(x => x.Email == loginViewModel.Email && x.Password == loginViewModel.Password && x.IsActive);
                    if (account == null)
                    {
                        ModelState.AddModelError("", "Giriş Başarısız!");
                    }
                    else
                    {
                        //Chaim kütüphanesi ile kullanıcıya yetki tanımlıyoruz. Dizi oluşturuyoruz.
                        var claims = new List<Claim>()
                        {
                            new(ClaimTypes.Name, account.Name),
                            new(ClaimTypes.Role, account.IsAdmin ? "Admin" : "Customer"),
                            new(ClaimTypes.Email, account.Email),
                            new("UserGuid", account.UserGuid.ToString()),
                            new("UserId", account.Id.ToString())
                        };

                        //kullanıcı için identity oluşturuyoruz. Login metodu olduğunu belirtiyoruz.
                        var userIdentity= new ClaimsIdentity(claims, "Login");
                        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(userIdentity);
                        await HttpContext.SignInAsync(claimsPrincipal);
                    //Redirect ile urlde yer alan ReturnUrl!i yakalıyoruz. Admin ise kullanıcı admin sayfasına yönlendiriyoruz.
                        return Redirect(string.IsNullOrEmpty(loginViewModel.ReturnUrl) ? "/" : loginViewModel.ReturnUrl);
                    }
                }
                catch (Exception hata)
                {
                    ModelState.AddModelError("", "Hata Oluştu.");
                }
            }
            return View(loginViewModel);
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUpAsync(AppUser appUser)
        {
            appUser.IsAdmin = false;
            appUser.IsActive = true;
            if (ModelState.IsValid)
            {
                await _context.AddAsync(appUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appUser);

        }

        public async Task<IActionResult> SignOutAsync()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("SignIn");
        }
    }
}
