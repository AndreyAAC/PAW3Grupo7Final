using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Mvc.Models.Home;
using ProyectoFinal.Models.DTOs;
using System.Net.Http.Json;

public class HomeController : Controller
{
    private readonly IHttpClientFactory _clientFactory;

    public HomeController(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    private HttpClient ApiGastos => _clientFactory.CreateClient("ApiGastos");
    private HttpClient ApiCuentas => _clientFactory.CreateClient("ApiCuentas");
    private HttpClient ApiProductos => _clientFactory.CreateClient("ApiProductos");

    public async Task<IActionResult> Index()
    {
        var vm = new DashboardVM();

        // Total de productos
        var productos = await ApiProductos.GetFromJsonAsync<List<ProductoDTO>>("api/Productos");
        vm.TotalProductos = productos?.Count ?? 0;

        // Gastos de HOY

        var gastos = await ApiGastos.GetFromJsonAsync<List<GastoDTO>>("api/Gastos");

        vm.GastosHoy = gastos?
            .Where(g => g.FechaGasto == DateOnly.FromDateTime(DateTime.Today))
            .Sum(g => g.Monto) ?? 0;

        // Cuentas pendientes
 
        var cuentas = await ApiCuentas.GetFromJsonAsync<List<CuentaPagarDTO>>("cuentas-pagar");

        vm.CuentasPendientes = cuentas?.Count ?? 0;

        return View(vm);
    }
}