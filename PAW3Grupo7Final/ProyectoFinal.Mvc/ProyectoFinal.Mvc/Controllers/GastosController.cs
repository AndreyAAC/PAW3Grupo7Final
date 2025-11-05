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

        // GET: /Gastos
        public async Task<IActionResult> Index()
        {
            var api = _httpClientFactory.CreateClient("ApiGastos");
            var data = await api.GetFromJsonAsync<List<GastoDTO>>("api/gastos");
            return View("~/Views/Contabilidad/Gastos.cshtml", new GastosVM { Gastos = data ?? new() });
        }

        // GET: /Gastos/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var api = _httpClientFactory.CreateClient("ApiGastos");
            var dto = await api.GetFromJsonAsync<GastoDTO>($"api/gastos/{id}");
            if (dto is null) return NotFound();
            return PartialView("~/Views/Contabilidad/_EditGasto.cshtml", dto);
        }

        // POST: /Gastos/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GastoDTO dto)
        {
            var api = _httpClientFactory.CreateClient("ApiGastos");
            await api.PutAsJsonAsync($"api/gastos/{dto.IdGasto}", dto);
            return RedirectToAction(nameof(Index));
        }

        // POST: /Gastos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var api = _httpClientFactory.CreateClient("ApiGastos");
            await api.DeleteAsync($"api/gastos/{id}");
            return RedirectToAction(nameof(Index));
        }

        // POST: /Gastos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GastoDTO dto)
        {
            var api = _httpClientFactory.CreateClient("ApiGastos");
            await api.PostAsJsonAsync("api/gastos", dto);
            return RedirectToAction(nameof(Index));
        }
    }
}