using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Data.Models;
using ProyectoFinal.Models.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.Core.BusinessLogic
{
    public interface IPedidoBusiness
    {
        Task<IEnumerable<PedidoHistorialDTO>> GetHistorialAsync(
            DateOnly? fechaInicio = null,
            DateOnly? fechaFin = null,
            int? idEstadoPedido = null);

        Task<PedidoHistorialDTO?> GetByIdAsync(int id);
    }

    public class PedidoBusiness : IPedidoBusiness
    {
        private readonly ControlInventarioDBContext _ctx;

        public PedidoBusiness(ControlInventarioDBContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<PedidoHistorialDTO>> GetHistorialAsync(
            DateOnly? fechaInicio = null,
            DateOnly? fechaFin = null,
            int? idEstadoPedido = null)
        {
            var query = _ctx.Pedidos.AsQueryable();

            if (fechaInicio.HasValue)
            {
                query = query.Where(p => p.FechaDeInicio >= fechaInicio.Value);
            }

            if (fechaFin.HasValue)
            {
                query = query.Where(p => p.FechaDeInicio <= fechaFin.Value);
            }

            if (idEstadoPedido.HasValue)
            {
                query = query.Where(p => p.IdEstadoPedido == idEstadoPedido.Value);
            }

            var result = await (
                from p in query
                join e in _ctx.EstadosPedido
                    on p.IdEstadoPedido equals e.IdEstadoPedido
                join d in _ctx.PedidoDetalles
                    on p.IdPedido equals d.IdPedido into detalles
                orderby p.FechaDeInicio descending, p.IdPedido descending
                select new PedidoHistorialDTO
                {
                    IdPedido = p.IdPedido,
                    FechaDeInicio = p.FechaDeInicio,
                    FechaDeEntrega = p.FechaDeEntrega,
                    IdEstadoPedido = p.IdEstadoPedido,
                    NombreEstado = e.NombreEstado,
                    CantidadProductos = detalles.Sum(x => (int?)x.Cantidad) ?? 0,
                    Total = detalles.Sum(x => (decimal?)(x.Cantidad * x.PrecioUnitario)) ?? 0m
                }
            ).ToListAsync();

            return result;
        }

        public async Task<PedidoHistorialDTO?> GetByIdAsync(int id)
        {
            var result = await (
                from p in _ctx.Pedidos
                join e in _ctx.EstadosPedido
                    on p.IdEstadoPedido equals e.IdEstadoPedido
                join d in _ctx.PedidoDetalles
                    on p.IdPedido equals d.IdPedido into detalles
                where p.IdPedido == id
                select new PedidoHistorialDTO
                {
                    IdPedido = p.IdPedido,
                    FechaDeInicio = p.FechaDeInicio,
                    FechaDeEntrega = p.FechaDeEntrega,
                    IdEstadoPedido = p.IdEstadoPedido,
                    NombreEstado = e.NombreEstado,
                    CantidadProductos = detalles.Sum(x => (int?)x.Cantidad) ?? 0,
                    Total = detalles.Sum(x => (decimal?)(x.Cantidad * x.PrecioUnitario)) ?? 0m
                }
            ).FirstOrDefaultAsync();

            return result;
        }
    }
}
