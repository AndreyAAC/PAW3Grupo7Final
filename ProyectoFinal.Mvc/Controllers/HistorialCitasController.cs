using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Models.DTOs;
using ProyectoFinal.Mvc.Models.Historial;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ProyectoFinal.Mvc.Controllers
{
    public class HistorialCitasController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HistorialCitasController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: /HistorialCitas
        public async Task<IActionResult> Index()
        {
            var api = _httpClientFactory.CreateClient("ApiCitas");
            var data = await api.GetFromJsonAsync<List<CitaHistorialDTO>>("citas-historial");

            var vm = new HistorialCitasVM
            {
                Citas = data ?? new List<CitaHistorialDTO>()
            };

            return View("~/Views/Historial/Citas.cshtml", vm);
        }
    }
}
