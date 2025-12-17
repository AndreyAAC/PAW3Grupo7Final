using System.Text;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Models.DTOs;
using System.Net.Http.Json;
using ClosedXML.Excel;

namespace ProyectoFinal.Mvc.Controllers
{
    public class ReportesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ReportesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: /Reportes
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ExportInventarioExcel()
        {
            var api = _httpClientFactory.CreateClient("ApiInventarios");

            var inventario = await api
                .GetFromJsonAsync<List<InventarioDTO>>("api/inventarios")
                ?? new List<InventarioDTO>();

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Inventario");

            // Encabezados
            worksheet.Cell(1, 1).Value = "IdInventario";
            worksheet.Cell(1, 2).Value = "IdProducto";
            worksheet.Cell(1, 3).Value = "NombreProducto";
            worksheet.Cell(1, 4).Value = "Cantidad";

            // Filas
            var row = 2;
            foreach (var item in inventario)
            {
                worksheet.Cell(row, 1).Value = item.IdInventario;
                worksheet.Cell(row, 2).Value = item.IdProducto;
                worksheet.Cell(row, 3).Value = item.NombreProducto ?? string.Empty;
                worksheet.Cell(row, 4).Value = item.Cantidad;
                row++;
            }

            // Formato de tabla
            var range = worksheet.Range(1, 1, row - 1, 4);
            range.CreateTable();

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            var fileName = $"Inventario_{DateTime.Now:yyyyMMddHHmmss}.xlsx";

            return File(
                content,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName
            );
        }


        [HttpGet]
        public async Task<IActionResult> ExportGastosExcel()
        {
            var api = _httpClientFactory.CreateClient("ApiGastos");

            var gastos = await api
                .GetFromJsonAsync<List<GastoDTO>>("api/gastos")
                ?? new List<GastoDTO>();

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Gastos");

            // Encabezados
            worksheet.Cell(1, 1).Value = "IdGasto";
            worksheet.Cell(1, 2).Value = "Motivo";
            worksheet.Cell(1, 3).Value = "FechaGasto";
            worksheet.Cell(1, 4).Value = "Descripcion";
            worksheet.Cell(1, 5).Value = "Monto";
            worksheet.Cell(1, 6).Value = "Categoria";

            var row = 2;
            foreach (var g in gastos)
            {
                worksheet.Cell(row, 1).Value = g.IdGasto;
                worksheet.Cell(row, 2).Value = g.Motivo ?? string.Empty;
                worksheet.Cell(row, 3).Value = g.FechaGasto.ToString("yyyy-MM-dd");
                worksheet.Cell(row, 4).Value = g.Descripcion ?? string.Empty;
                worksheet.Cell(row, 5).Value = g.Monto;
                worksheet.Cell(row, 6).Value = g.NombreCategoria ?? string.Empty;
                row++;
            }

            var range = worksheet.Range(1, 1, row - 1, 6);
            range.CreateTable();
            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            var fileName = $"Gastos_{DateTime.Now:yyyyMMddHHmmss}.xlsx";

            return File(
                content,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName
            );
        }

        [HttpGet]
        public async Task<IActionResult> ExportCuentasPagarExcel()
        {
            var api = _httpClientFactory.CreateClient("ApiCuentas");

            var cuentas = await api
                .GetFromJsonAsync<List<CuentaPagarDTO>>("cuentas-pagar")
                ?? new List<CuentaPagarDTO>();

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Cuentas por Pagar");

            // Encabezados
            worksheet.Cell(1, 1).Value = "IdCuentaPagar";
            worksheet.Cell(1, 2).Value = "Motivo";
            worksheet.Cell(1, 3).Value = "Fecha";
            worksheet.Cell(1, 4).Value = "Descripcion";
            worksheet.Cell(1, 5).Value = "Monto";
            worksheet.Cell(1, 6).Value = "PlazoPagar";

            var row = 2;
            foreach (var c in cuentas)
            {
                worksheet.Cell(row, 1).Value = c.IdCuentaPagar;
                worksheet.Cell(row, 2).Value = c.Motivo ?? string.Empty;
                worksheet.Cell(row, 3).Value = c.FechaCuentaPagar.ToString("yyyy-MM-dd");
                worksheet.Cell(row, 4).Value = c.Descripcion ?? string.Empty;
                worksheet.Cell(row, 5).Value = c.Monto;
                worksheet.Cell(row, 6).Value = c.PlazoPagar ?? string.Empty;
                row++;
            }

            var range = worksheet.Range(1, 1, row - 1, 6);
            range.CreateTable();
            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            var fileName = $"CuentasPagar_{DateTime.Now:yyyyMMddHHmmss}.xlsx";

            return File(
                content,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName
            );
        }

        [HttpGet]
        public IActionResult ExportHistorialesExcel()
        {
            return Content(
                "El reporte de Historiales todavía no está implementado " +
                "porque no existe una tabla/endpoint de historiales en el proyecto actual. " +
                "Cuando lo agregués, podés replicar la lógica de los otros reportes.",
                "text/plain",
                Encoding.UTF8
            );
        }
    }
}
