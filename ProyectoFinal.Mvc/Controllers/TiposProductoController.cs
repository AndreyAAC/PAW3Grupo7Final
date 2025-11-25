using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Models.DTOs;
using ProyectoFinal.Mvc.Models.Inventario;
using System.Net.Http;
using System.Net.Http.Json;

namespace ProyectoFinal.Mvc.Controllers
{
    public class TiposProductoController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public TiposProductoController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        private HttpClient GetClient() => _clientFactory.CreateClient("ApiTiposProducto");

        // GET: /TiposProducto
        public async Task<IActionResult> Index()
        {
            var client = GetClient();

            var tipos = await client.GetFromJsonAsync<List<TipoProductoDTO>>("api/TiposProducto")
                        ?? new List<TipoProductoDTO>();

            var vm = new TiposProductoVM
            {
                Tipos = tipos
            };

            return View(vm);
        }

        // GET: /TiposProducto/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var client = GetClient();
            var dto = await client.GetFromJsonAsync<TipoProductoDTO>($"api/TiposProducto/{id}");

            if (dto == null)
                return NotFound();

            return PartialView("_EditTipoProducto", dto);
        }

        // POST: /TiposProducto/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TipoProductoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_EditTipoProducto", dto);
            }

            var client = GetClient();
            var response = await client.PutAsJsonAsync($"api/TiposProducto/{dto.IdTipoProducto}", dto);
            response.EnsureSuccessStatusCode();

            return RedirectToAction(nameof(Index));
        }

        // POST: /TiposProducto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TipoProductoDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.NombreTipo))
            {
                return RedirectToAction(nameof(Index));
            }

            var client = GetClient();
            var response = await client.PostAsJsonAsync("api/TiposProducto", dto);
            response.EnsureSuccessStatusCode();

            return RedirectToAction(nameof(Index));
        }

        // POST: /TiposProducto/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var client = GetClient();
            var response = await client.DeleteAsync($"api/TiposProducto/{id}");
            response.EnsureSuccessStatusCode();

            return RedirectToAction(nameof(Index));
        }
    }
}
