using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Models.DTOs;
using ProyectoFinal.Mvc.Models.Inventario;
using System.Net.Http;
using System.Net.Http.Json;

namespace ProyectoFinal.Mvc.Controllers
{
    public class InventarioController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public InventarioController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        private HttpClient GetInventariosClient() => _clientFactory.CreateClient("ApiInventarios");
        private HttpClient GetProductosClient() => _clientFactory.CreateClient("ApiProductos");

        // GET: /Inventario
        public async Task<IActionResult> Index()
        {
            var inventariosClient = GetInventariosClient();
            var productosClient = GetProductosClient();

            var inventarios = await inventariosClient.GetFromJsonAsync<List<InventarioDTO>>("api/inventarios")
                              ?? new List<InventarioDTO>();

            var productos = await productosClient.GetFromJsonAsync<List<ProductoDTO>>("api/productos")
                           ?? new List<ProductoDTO>();

            var vm = new InventariosVM
            {
                Inventarios = inventarios,
                Productos = productos
            };

            return View(vm); // Views/Inventario/Index.cshtml
        }

        // GET: /Inventario/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var inventariosClient = GetInventariosClient();
            var productosClient = GetProductosClient();

            var inventario = await inventariosClient.GetFromJsonAsync<InventarioDTO>($"api/inventarios/{id}");
            if (inventario == null)
                return NotFound();

            var productos = await productosClient.GetFromJsonAsync<List<ProductoDTO>>("api/productos")
                           ?? new List<ProductoDTO>();

            var vm = new InventarioEditVM
            {
                Inventario = inventario,
                Productos = productos
            };

            return PartialView("_EditInventario", vm);
        }

        // POST: /Inventario/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int IdInventario, int IdProducto, int Cantidad)
        {
            if (IdInventario <= 0 || IdProducto <= 0 || Cantidad < 0)
            {
                return RedirectToAction(nameof(Index));
            }

            var dto = new InventarioDTO
            {
                IdInventario = IdInventario,
                IdProducto = IdProducto,
                Cantidad = Cantidad
            };

            var client = GetInventariosClient();
            var response = await client.PutAsJsonAsync($"api/inventarios/{IdInventario}", dto);

            // Si quieres manejar errores sin romper, puedes reemplazar esto por un if (!response.IsSuccessStatusCode)...
            response.EnsureSuccessStatusCode();

            return RedirectToAction(nameof(Index));
        }

        // POST: /Inventario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int IdProducto, int Cantidad)
        {
            if (IdProducto <= 0 || Cantidad < 0)
            {
                TempData["ErrorInventario"] = "Debes seleccionar un producto válido y una cantidad mayor o igual a 0.";
                return RedirectToAction(nameof(Index));
            }

            var dto = new InventarioDTO
            {
                IdProducto = IdProducto,
                Cantidad = Cantidad
            };

            var client = GetInventariosClient();
            var response = await client.PostAsJsonAsync("api/inventarios", dto);

            if (!response.IsSuccessStatusCode)
            {
                var detalle = await response.Content.ReadAsStringAsync();
                // Guardamos el detalle para verlo en la vista
                TempData["ErrorInventario"] =
                    $"Error al crear inventario. Status: {(int)response.StatusCode} ({response.StatusCode}). Detalle: {detalle}";
                return RedirectToAction(nameof(Index));
            }

            TempData["OkInventario"] = "Inventario creado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Inventario/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var client = GetInventariosClient();
            var response = await client.DeleteAsync($"api/inventarios/{id}");
            response.EnsureSuccessStatusCode();

            return RedirectToAction(nameof(Index));
        }
    }
}
