using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ProyectoCalendarizacionDeVacaciones.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();
builder.Services.AddDbContext<VacacionescfeContext>(options => options.UseMySql("server=localhost;user=root;password=root;database=ProyectoCFE", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql")));


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });
var app = builder.Build();

app.UseAuthentication(); 
app.UseAuthorization();
app.UseStaticFiles();
app.MapDefaultControllerRoute();
app.MapAreaControllerRoute(
      name: "areas",
      areaName: "Admin",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
  );
app.Run();
