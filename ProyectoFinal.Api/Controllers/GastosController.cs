using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Data.Models;
using ProyectoFinal.Models.DTOs;

namespace ProyectoFinal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GastosController : ControllerBase
    {
        private readonly ControlInventarioDBContext _ctx;

        public GastosController(ControlInventarioDBContext ctx)
        {
            _ctx = ctx;
        }

        // GET: api/gastos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GastoDTO>>> GetAll()
        {
            var query = from g in _ctx.Gastos
                        join c in _ctx.CategoriasGasto on g.IdCategoriaGasto equals c.IdCategoriaGasto into cg
                        from c in cg.DefaultIfEmpty()
                        orderby g.FechaGasto descending, g.IdGasto descending
                        select new GastoDTO
                        {
                            IdGasto = g.IdGasto,
                            Motivo = g.Motivo,
                            FechaGasto = g.FechaGasto,
                            Descripcion = g.Descripcion,
                            Monto = g.Monto,
                            IdCategoriaGasto = g.IdCategoriaGasto,
                            NombreCategoria = c != null ? c.NombreCategoria : null
                        };

            return Ok(await query.ToListAsync());
        }

        // GET: api/gastos/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GastoDTO>> GetById(int id)
        {
            var result = await (from g in _ctx.Gastos
                                join c in _ctx.CategoriasGasto on g.IdCategoriaGasto equals c.IdCategoriaGasto into cg
                                from c in cg.DefaultIfEmpty()
                                where g.IdGasto == id
                                select new GastoDTO
                                {
                                    IdGasto = g.IdGasto,
                                    Motivo = g.Motivo,
                                    FechaGasto = g.FechaGasto,
                                    Descripcion = g.Descripcion,
                                    Monto = g.Monto,
                                    IdCategoriaGasto = g.IdCategoriaGasto,
                                    NombreCategoria = c != null ? c.NombreCategoria : null
                                }).FirstOrDefaultAsync();

            if (result == null) return NotFound();
            return Ok(result);
        }

        // POST: api/gastos
        [HttpPost]
        public async Task<ActionResult<bool>> Create([FromBody] GastoDTO dto)
        {
            if (dto is null) return BadRequest(false);

            var entity = new Gasto
            {
                Motivo = dto.Motivo,
                FechaGasto = dto.FechaGasto,
                Descripcion = dto.Descripcion,
                Monto = dto.Monto,
                IdCategoriaGasto = dto.IdCategoriaGasto
            };

            _ctx.Gastos.Add(entity);
            await _ctx.SaveChangesAsync();
            return Ok(true);
        }

        // PUT: api/gastos/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<bool>> Update(int id, [FromBody] GastoDTO dto)
        {
            var entity = await _ctx.Gastos.FindAsync(id);
            if (entity == null) return NotFound(false);

            entity.Motivo = dto.Motivo;
            entity.FechaGasto = dto.FechaGasto;
            entity.Descripcion = dto.Descripcion;
            entity.Monto = dto.Monto;
            entity.IdCategoriaGasto = dto.IdCategoriaGasto;

            await _ctx.SaveChangesAsync();
            return Ok(true);
        }

        // DELETE: api/gastos/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var entity = await _ctx.Gastos.FindAsync(id);
            if (entity == null) return NotFound(false);

            _ctx.Gastos.Remove(entity);
            await _ctx.SaveChangesAsync();
            return Ok(true);
        }
    }
}