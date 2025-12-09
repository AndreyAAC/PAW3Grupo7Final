using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Data.Models;
using ProyectoFinal.Models.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.Core.BusinessLogic
{
    public interface ICitaBusiness
    {
        Task<IEnumerable<CitaHistorialDTO>> GetHistorialAsync(
            DateOnly? fechaInicio = null,
            DateOnly? fechaFin = null,
            int? idCliente = null);

        Task<CitaHistorialDTO?> GetByIdAsync(int id);
    }

    public class CitaBusiness : ICitaBusiness
    {
        private readonly ControlInventarioDBContext _ctx;

        public CitaBusiness(ControlInventarioDBContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<CitaHistorialDTO>> GetHistorialAsync(
            DateOnly? fechaInicio = null,
            DateOnly? fechaFin = null,
            int? idCliente = null)
        {
            var query = _ctx.Citas.AsQueryable();

            if (fechaInicio.HasValue)
            {
                query = query.Where(c => c.FechaCita >= fechaInicio.Value);
            }

            if (fechaFin.HasValue)
            {
                query = query.Where(c => c.FechaCita <= fechaFin.Value);
            }

            if (idCliente.HasValue)
            {
                query = query.Where(c => c.IdCliente == idCliente.Value);
            }

            var result = await (
                from c in query
                join cli in _ctx.Clientes
                    on c.IdCliente equals cli.IdCliente
                join p in _ctx.Productos
                    on c.Producto equals p.IdProducto into prodJoin
                from p in prodJoin.DefaultIfEmpty()
                orderby c.FechaCita descending, c.HoraCita descending, c.IdCita descending
                select new CitaHistorialDTO
                {
                    IdCita = c.IdCita,
                    IdCliente = c.IdCliente,
                    NombreCliente = cli.Nombre,
                    Motivo = c.Motivo,
                    IdProducto = c.Producto,
                    NombreProducto = p != null ? p.Nombre : null,
                    Detalle = c.Detalle,
                    FechaCita = c.FechaCita,
                    HoraCita = c.HoraCita
                }
            ).ToListAsync();

            return result;
        }

        public async Task<CitaHistorialDTO?> GetByIdAsync(int id)
        {
            var result = await (
                from c in _ctx.Citas
                join cli in _ctx.Clientes
                    on c.IdCliente equals cli.IdCliente
                join p in _ctx.Productos
                    on c.Producto equals p.IdProducto into prodJoin
                from p in prodJoin.DefaultIfEmpty()
                where c.IdCita == id
                select new CitaHistorialDTO
                {
                    IdCita = c.IdCita,
                    IdCliente = c.IdCliente,
                    NombreCliente = cli.Nombre,
                    Motivo = c.Motivo,
                    IdProducto = c.Producto,
                    NombreProducto = p != null ? p.Nombre : null,
                    Detalle = c.Detalle,
                    FechaCita = c.FechaCita,
                    HoraCita = c.HoraCita
                }
            ).FirstOrDefaultAsync();

            return result;
        }
    }
}
