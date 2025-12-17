using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Models.DTOs;
using ProyectoFinal.Mvc.Models.Historial;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ProyectoFinal.Mvc.Controllers
{
    public class HistorialPedidosController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HistorialPedidosController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: /HistorialPedidos
        public async Task<IActionResult> Index(DateOnly? fechaInicio, DateOnly? fechaFin, int? idEstadoPedido)
        {
            var api = _httpClientFactory.CreateClient("ApiPedidos");

            string url = "api/pedidos/historial";

            var queryParams = new List<string>();
            if (fechaInicio.HasValue) queryParams.Add($"fechaInicio={fechaInicio:yyyy-MM-dd}");
            if (fechaFin.HasValue) queryParams.Add($"fechaFin={fechaFin:yyyy-MM-dd}");
            if (idEstadoPedido.HasValue) queryParams.Add($"idEstadoPedido={idEstadoPedido}");

            if (queryParams.Count > 0)
            {
                url += "?" + string.Join("&", queryParams);
            }

            var data = await api.GetFromJsonAsync<List<PedidoHistorialDTO>>(url);

            var vm = new HistorialPedidosVM
            {
                Pedidos = data ?? new List<PedidoHistorialDTO>()
            };

            return View("~/Views/Historial/Pedidos.cshtml", vm);
        }
    }
}
