using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Models.DTOs;
using ProyectoFinal.Mvc.Models.Contabilidad;
using System.Net.Http.Json;

namespace ProyectoFinal.Mvc.Controllers
{
    public class GastosController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GastosController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private HttpClient GetGastosClient() => _httpClientFactory.CreateClient("ApiGastos");
        private HttpClient GetCategoriasClient() => _httpClientFactory.CreateClient("ApiCategoriasGasto");

        // GET: /Gastos
        public async Task<IActionResult> Index()
        {
            var apiGastos = GetGastosClient();
            var apiCategorias = GetCategoriasClient();

            var gastos = await apiGastos.GetFromJsonAsync<List<GastoDTO>>("api/gastos")
                        ?? new List<GastoDTO>();

            var categorias = await apiCategorias.GetFromJsonAsync<List<CategoriaGastoDTO>>("api/CategoriasGasto")
                            ?? new List<CategoriaGastoDTO>();

            var vm = new GastosVM
            {
                Gastos = gastos,
                Categorias = categorias
            };

            return View("~/Views/Contabilidad/Gastos.cshtml", vm);
        }

        // GET: /Gastos/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var apiGastos = GetGastosClient();
            var apiCategorias = GetCategoriasClient();

            var dto = await apiGastos.GetFromJsonAsync<GastoDTO>($"api/gastos/{id}");
            if (dto is null) return NotFound();

            var categorias = await apiCategorias.GetFromJsonAsync<List<CategoriaGastoDTO>>("api/CategoriasGasto")
                            ?? new List<CategoriaGastoDTO>();

            ViewBag.Categorias = categorias;

            return PartialView("~/Views/Contabilidad/_EditGasto.cshtml", dto);
        }

        // POST: /Gastos/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GastoDTO dto)
        {
            var api = GetGastosClient();
            await api.PutAsJsonAsync($"api/gastos/{dto.IdGasto}", dto);
            return RedirectToAction(nameof(Index));
        }

        // POST: /Gastos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var api = GetGastosClient();
            await api.DeleteAsync($"api/gastos/{id}");
            return RedirectToAction(nameof(Index));
        }

        // POST: /Gastos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GastoDTO dto)
        {
            var api = GetGastosClient();
            await api.PostAsJsonAsync("api/gastos", dto);
            return RedirectToAction(nameof(Index));
        }
    }
}