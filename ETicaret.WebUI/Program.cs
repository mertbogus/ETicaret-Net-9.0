using ETicaret.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DatabaseContext>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
    //login yolunu belirtiyoruz.
    x.LoginPath = "/Account/SignIn";
    //yetkisiz eriþimleri yönlendirme url
    x.AccessDeniedPath = "/AccesDenied";
    //cookie isim veriyoruz
    x.Cookie.Name = "Account";
    //cookie yaþam süresi veriyoruz. 1 Gün verdim.
    x.Cookie.MaxAge = TimeSpan.FromDays(1);
    //Kalýcý Cookie oluþturmak için
    x.Cookie.IsEssential = true;
});
builder.Services.AddAuthorization(x =>
{
    //Yetki rollerini tanýmlýyoruz. Admininkini belirttik.
    x.AddPolicy("AdminPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
    //Burada User'inkini belirttik Ayrýca adminin de eriþmesini saðladýk.
    x.AddPolicy("UserPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Customer","Admin","User"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); //önce oturum açma gelmeli her zaman
app.UseAuthorization(); //oturum açýldýktan sonra yetkilendirme.
app.MapControllerRoute(
           name: "admin",
           pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
