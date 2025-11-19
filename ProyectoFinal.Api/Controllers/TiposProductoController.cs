using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Data.Models;
using ProyectoFinal.Models.DTOs;

namespace ProyectoFinal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TiposProductoController : ControllerBase
    {
        private readonly ControlInventarioDBContext _ctx;

        public TiposProductoController(ControlInventarioDBContext ctx)
        {
            _ctx = ctx;
        }

        // GET: api/TiposProducto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoProductoDTO>>> GetAll()
        {
            var lista = await _ctx.TiposProducto
                .OrderBy(t => t.NombreTipo)
                .Select(t => new TipoProductoDTO
                {
                    IdTipoProducto = t.IdTipoProducto,
                    NombreTipo = t.NombreTipo
                })
                .ToListAsync();

            return Ok(lista);
        }

        // GET: api/TiposProducto/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TipoProductoDTO>> GetById(int id)
        {
            var dto = await _ctx.TiposProducto
                .Where(t => t.IdTipoProducto == id)
                .Select(t => new TipoProductoDTO
                {
                    IdTipoProducto = t.IdTipoProducto,
                    NombreTipo = t.NombreTipo
                })
                .FirstOrDefaultAsync();

            if (dto == null) return NotFound();

            return Ok(dto);
        }

        // POST: api/TiposProducto
        [HttpPost]
        public async Task<ActionResult<bool>> Create([FromBody] TipoProductoDTO dto)
        {
            if (dto is null || string.IsNullOrWhiteSpace(dto.NombreTipo))
                return BadRequest(false);

            var entity = new TipoProducto
            {
                NombreTipo = dto.NombreTipo
            };

            _ctx.TiposProducto.Add(entity);
            await _ctx.SaveChangesAsync();

            return Ok(true);
        }

        // PUT: api/TiposProducto/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<bool>> Update(int id, [FromBody] TipoProductoDTO dto)
        {
            var entity = await _ctx.TiposProducto.FindAsync(id);
            if (entity == null) return NotFound(false);

            if (string.IsNullOrWhiteSpace(dto.NombreTipo))
                return BadRequest(false);

            entity.NombreTipo = dto.NombreTipo;
            await _ctx.SaveChangesAsync();

            return Ok(true);
        }

        // DELETE: api/TiposProducto/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var entity = await _ctx.TiposProducto.FindAsync(id);
            if (entity == null) return NotFound(false);

            _ctx.TiposProducto.Remove(entity);
            await _ctx.SaveChangesAsync();
            return Ok(true);
        }
    }
}
