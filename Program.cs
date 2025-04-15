using Microsoft.EntityFrameworkCore;
using ProyectoCalendarizacionDeVacaciones.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();
builder.Services.AddDbContext<ProyectoCfeContext>(options => options.UseMySql("server=localhost;user=root;password=root;database=ProyectoCFE", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql")));
var app = builder.Build();

app.UseStaticFiles();
app.MapDefaultControllerRoute();
app.Run();
