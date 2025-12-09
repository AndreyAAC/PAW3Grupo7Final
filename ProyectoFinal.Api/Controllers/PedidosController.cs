using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Core.BusinessLogic;
using ProyectoFinal.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProyectoFinal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoBusiness _pedidoBusiness;

        public PedidosController(IPedidoBusiness pedidoBusiness)
        {
            _pedidoBusiness = pedidoBusiness;
        }

        // GET: api/pedidos/historial
        [HttpGet("historial")]
        public async Task<ActionResult<IEnumerable<PedidoHistorialDTO>>> GetHistorial(
            [FromQuery] DateOnly? fechaInicio,
            [FromQuery] DateOnly? fechaFin,
            [FromQuery] int? idEstadoPedido)
        {
            var data = await _pedidoBusiness.GetHistorialAsync(fechaInicio, fechaFin, idEstadoPedido);
            return Ok(data);
        }

        // GET: api/pedidos/historial/5
        [HttpGet("historial/{id:int}")]
        public async Task<ActionResult<PedidoHistorialDTO>> GetById(int id)
        {
            var pedido = await _pedidoBusiness.GetByIdAsync(id);
            if (pedido == null)
                return NotFound();

            return Ok(pedido);
        }
    }
}
