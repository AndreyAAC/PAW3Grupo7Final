using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Data.Models;
using ProyectoFinal.Models.DTOs;

namespace ProyectoFinal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly ControlInventarioDBContext _ctx;

        public ProductosController(ControlInventarioDBContext ctx)
        {
            _ctx = ctx;
        }

        // GET: api/productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> GetAll()
        {
            var query = from p in _ctx.Productos
                        join t in _ctx.TiposProducto on p.IdTipoProducto equals t.IdTipoProducto into pt
                        from t in pt.DefaultIfEmpty()
                        orderby p.Nombre
                        select new ProductoDTO
                        {
                            IdProducto = p.IdProducto,
                            Nombre = p.Nombre,
                            Imagen = p.Imagen,
                            Descripcion = p.Descripcion,
                            Precio = p.Precio,
                            IdTipoProducto = p.IdTipoProducto,
                            NombreTipoProducto = t != null ? t.NombreTipo : null
                        };

            var lista = await query.ToListAsync();
            return Ok(lista);
        }

        // GET: api/productos/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductoDTO>> GetById(int id)
        {
            var query = from p in _ctx.Productos
                        join t in _ctx.TiposProducto on p.IdTipoProducto equals t.IdTipoProducto into pt
                        from t in pt.DefaultIfEmpty()
                        where p.IdProducto == id
                        select new ProductoDTO
                        {
                            IdProducto = p.IdProducto,
                            Nombre = p.Nombre,
                            Imagen = p.Imagen,
                            Descripcion = p.Descripcion,
                            Precio = p.Precio,
                            IdTipoProducto = p.IdTipoProducto,
                            NombreTipoProducto = t != null ? t.NombreTipo : null
                        };

            var dto = await query.FirstOrDefaultAsync();
            if (dto == null) return NotFound();

            return Ok(dto);
        }

        // POST: api/productos
        [HttpPost]
        public async Task<ActionResult<bool>> Create([FromBody] ProductoDTO dto)
        {
            if (dto is null) return BadRequest(false);
            if (string.IsNullOrWhiteSpace(dto.Nombre)) return BadRequest(false);

            var entity = new Producto
            {
                Nombre = dto.Nombre,
                Imagen = dto.Imagen,
                Descripcion = dto.Descripcion,
                Precio = dto.Precio,
                IdTipoProducto = dto.IdTipoProducto
            };

            _ctx.Productos.Add(entity);
            await _ctx.SaveChangesAsync();

            // si quisieras devolver el nuevo id:
            // return Ok(entity.IdProducto);
            return Ok(true);
        }

        // PUT: api/productos/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<bool>> Update(int id, [FromBody] ProductoDTO dto)
        {
            var entity = await _ctx.Productos.FindAsync(id);
            if (entity == null) return NotFound(false);

            entity.Nombre = dto.Nombre;
            entity.Imagen = dto.Imagen;
            entity.Descripcion = dto.Descripcion;
            entity.Precio = dto.Precio;
            entity.IdTipoProducto = dto.IdTipoProducto;

            await _ctx.SaveChangesAsync();
            return Ok(true);
        }

        // DELETE: api/productos/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var entity = await _ctx.Productos.FindAsync(id);
            if (entity == null) return NotFound(false);

            _ctx.Productos.Remove(entity);
            await _ctx.SaveChangesAsync();
            return Ok(true);
        }
    }
}
