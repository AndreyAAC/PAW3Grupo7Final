using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Models.DTOs;
using System.Net.Http.Json;

namespace ProyectoFinal.Mvc.Controllers
{
    public class UsuariosAdminController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public UsuariosAdminController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        private HttpClient GetClient() => _clientFactory.CreateClient("ApiUsuarios");

        private bool UsuarioEsAdmin()
        {
            var roleId = HttpContext.Session.GetInt32("RoleId");
            return roleId == 2;
        }

        // GET: /UsuariosAdmin
        public async Task<IActionResult> Index()
        {
            if (!UsuarioEsAdmin())
                return RedirectToAction("Index", "Home");

            var client = GetClient();
            var usuarios = await client.GetFromJsonAsync<List<UsuarioAdminDTO>>("api/usuarios")
                           ?? new List<UsuarioAdminDTO>();

            return View(usuarios);
        }

        // GET: /UsuariosAdmin/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!UsuarioEsAdmin())
                return RedirectToAction("Index", "Home");

            var client = GetClient();
            var usuario = await client.GetFromJsonAsync<UsuarioAdminDTO>($"api/usuarios/{id}");
            if (usuario == null) return NotFound();

            return View(usuario);
        }

        // POST: /UsuariosAdmin/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UsuarioAdminDTO model)
        {
            if (!UsuarioEsAdmin())
                return RedirectToAction("Index", "Home");

            if (!ModelState.IsValid)
                return View(model);

            var client = GetClient();
            var response = await client.PutAsJsonAsync($"api/usuarios/{model.IdUsuario}", model);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "No se pudo guardar el usuario.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}