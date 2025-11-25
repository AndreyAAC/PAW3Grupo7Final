using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Models.DTOs;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;

namespace ProyectoFinal.Mvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public AccountController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        private HttpClient GetAuthClient() => _clientFactory.CreateClient("ApiAuth");

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UsuarioLoginDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var client = GetAuthClient();
            var response = await client.PostAsJsonAsync("api/Auth/login", model);

            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadFromJsonAsync<UsuarioLoginResultDTO>();
                if (user != null)
                {
                    HttpContext.Session.SetInt32("UserId", user.IdUsuario);
                    HttpContext.Session.SetString("UserName", user.Nombre);
                    HttpContext.Session.SetInt32("RoleId", user.RoleId);
                }

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Correo o contraseña incorrectos.");
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UsuarioRegisterDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.Contrasenia != model.ConfirmarContrasenia)
            {
                ModelState.AddModelError(string.Empty, "Las contraseñas no son iguales.");
                return View(model);
            }

            var client = GetAuthClient();
            var response = await client.PostAsJsonAsync("api/Auth/register", model);

            if (response.IsSuccessStatusCode)
            {
                TempData["RegisterSuccess"] = "Cuenta creada correctamente. Ya puedes iniciar sesión.";
                return RedirectToAction("Login");
            }

            ModelState.AddModelError(string.Empty, "No se pudo registrar el usuario. Verifica los datos.");
            return View(model);
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}