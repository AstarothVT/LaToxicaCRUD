using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using LaToxicaCRUD.Data;
using LaToxicaCRUD.Services;

var builder = WebApplication.CreateBuilder(args);

// Usa punto como separador decimal (precios como 12.50)
var cultura = System.Globalization.CultureInfo.InvariantCulture;
System.Globalization.CultureInfo.DefaultThreadCurrentCulture = cultura;
System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = cultura;

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ToxicaContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ConexionToxica")
    ));

builder.Services.AddScoped<ReporteService>();

// Login con cookies: protege Platos, Categorías y Reportes
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opciones =>
    {
        opciones.LoginPath = "/Account/Login";
        opciones.AccessDeniedPath = "/Account/Login";
        opciones.ExpireTimeSpan = TimeSpan.FromHours(2);
        opciones.SlidingExpiration = true;
    });

var app = builder.Build();

// Crea la base de datos con las migraciones y carga el menú inicial
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ToxicaContext>();
    context.Database.Migrate();
    DbInitializer.Sembrar(context);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
