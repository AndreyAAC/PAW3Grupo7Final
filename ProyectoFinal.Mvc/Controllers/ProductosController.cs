using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Models.DTOs;
using ProyectoFinal.Mvc.Models.Inventario;
using System.Net.Http;
using System.Net.Http.Json;

namespace ProyectoFinal.Mvc.Controllers
{
    public class ProductosController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public ProductosController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        private HttpClient GetProductosClient() => _clientFactory.CreateClient("ApiProductos");
        private HttpClient GetTiposClient() => _clientFactory.CreateClient("ApiTiposProducto");

        // GET: /Productos
        public async Task<IActionResult> Index()
        {
            var productosClient = GetProductosClient();
            var tiposClient = GetTiposClient();

            var productos = await productosClient.GetFromJsonAsync<List<ProductoDTO>>("api/productos")
                            ?? new List<ProductoDTO>();

            var tipos = await tiposClient.GetFromJsonAsync<List<TipoProductoDTO>>("api/TiposProducto")
                        ?? new List<TipoProductoDTO>();

            var vm = new ProductosVM
            {
                Productos = productos,
                TiposProducto = tipos
            };

            return View(vm); // Views/Productos/Index.cshtml
        }

        // GET: /Productos/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var productosClient = GetProductosClient();
            var tiposClient = GetTiposClient();

            var dto = await productosClient.GetFromJsonAsync<ProductoDTO>($"api/productos/{id}");
            if (dto == null)
                return NotFound();

            var tipos = await tiposClient.GetFromJsonAsync<List<TipoProductoDTO>>("api/TiposProducto")
                        ?? new List<TipoProductoDTO>();

            var vm = new ProductoEditVM
            {
                Producto = dto,
                TiposProducto = tipos
            };

            // Partial para el modal
            return PartialView("_EditProducto", vm);
        }

        // POST: /Productos/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductoEditVM vm)
        {
            if (!ModelState.IsValid)
            {
                // Si hay errores, devolvemos el mismo modal con la data
                return PartialView("_EditProducto", vm);
            }

            var client = GetProductosClient();
            var response = await client.PutAsJsonAsync($"api/productos/{vm.Producto.IdProducto}", vm.Producto);
            response.EnsureSuccessStatusCode();

            return RedirectToAction(nameof(Index));
        }

        // POST: /Productos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                // Para no complicarnos, redirigimos al Index, que recarga todo
                return RedirectToAction(nameof(Index));
            }

            var client = GetProductosClient();
            var response = await client.PostAsJsonAsync("api/productos", dto);
            response.EnsureSuccessStatusCode();

            return RedirectToAction(nameof(Index));
        }

        // POST: /Productos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var client = GetProductosClient();
            var response = await client.DeleteAsync($"api/productos/{id}");
            response.EnsureSuccessStatusCode();

            return RedirectToAction(nameof(Index));
        }
    }
}
