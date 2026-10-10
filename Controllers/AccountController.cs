using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using LaToxicaCRUD.Models;

namespace LaToxicaCRUD.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _config;

        public AccountController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Platos");

            ViewData["ReturnUrl"] = returnUrl;
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel modelo, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
                return View(modelo);

            // Usuario y contraseña del administrador (están en appsettings.json)
            var usuarioValido = _config["Admin:Usuario"];
            var claveValida = _config["Admin:Clave"];

            if (modelo.Usuario != usuarioValido || modelo.Clave != claveValida)
            {
                ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos");
                return View(modelo);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, modelo.Usuario),
                new Claim(ClaimTypes.Role, "Administrador")
            };

            var identidad = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identidad));

            TempData["Exito"] = "Bienvenido al panel de La Tóxica.";

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Platos");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["Exito"] = "Sesión cerrada correctamente.";
            return RedirectToAction("Index", "Home");
        }
    }
}
