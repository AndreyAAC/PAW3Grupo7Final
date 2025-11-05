using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Models.DTOs;
using ProyectoFinal.Mvc.Models.Contabilidad;
using System.Net.Http.Json;

namespace ProyectoFinal.Mvc.Controllers
{
    public class CuentasPagarController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CuentasPagarController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: /CuentasPagar
        public async Task<IActionResult> Index()
        {
            var api = _httpClientFactory.CreateClient("ApiCuentas");
            var data = await api.GetFromJsonAsync<List<CuentaPagarDTO>>("cuentas-pagar");
            return View("~/Views/Contabilidad/CuentaPagar.cshtml", new CuentasPagarVM { Cuentas = data ?? new() });
        }

        // GET: /CuentasPagar/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var api = _httpClientFactory.CreateClient("ApiCuentas");
            var dto = await api.GetFromJsonAsync<CuentaPagarDTO>($"cuentas-pagar/{id}");
            if (dto is null) return NotFound();
            return PartialView("~/Views/Contabilidad/_EditCuenta.cshtml", dto);
        }

        // POST: /CuentasPagar/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CuentaPagarDTO dto)
        {
            var api = _httpClientFactory.CreateClient("ApiCuentas");
            await api.PutAsJsonAsync($"cuentas-pagar/{dto.IdCuentaPagar}", dto);
            return RedirectToAction(nameof(Index));
        }

        // POST: /CuentasPagar/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var api = _httpClientFactory.CreateClient("ApiCuentas");
            await api.DeleteAsync($"cuentas-pagar/{id}");
            return RedirectToAction(nameof(Index));
        }

        // POST: /CuentasPagar/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CuentaPagarDTO dto)
        {
            var api = _httpClientFactory.CreateClient("ApiCuentas");
            await api.PostAsJsonAsync("cuentas-pagar", dto);
            return RedirectToAction(nameof(Index));
        }
    }
}