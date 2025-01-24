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
    //yetkisiz eri�imleri y�nlendirme url
    x.AccessDeniedPath = "/AccesDenied";
    //cookie isim veriyoruz
    x.Cookie.Name = "Account";
    //cookie ya�am s�resi veriyoruz. 1 G�n verdim.
    x.Cookie.MaxAge = TimeSpan.FromDays(1);
    //Kal�c� Cookie olu�turmak i�in
    x.Cookie.IsEssential = true;
});
builder.Services.AddAuthorization(x =>
{
    //Yetki rollerini tan�ml�yoruz. Admininkini belirttik.
    x.AddPolicy("AdminPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
    //Burada User'inkini belirttik Ayr�ca adminin de eri�mesini sa�lad�k.
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
app.UseAuthentication(); //�nce oturum a�ma gelmeli her zaman
app.UseAuthorization(); //oturum a��ld�ktan sonra yetkilendirme.
app.MapControllerRoute(
           name: "admin",
           pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
