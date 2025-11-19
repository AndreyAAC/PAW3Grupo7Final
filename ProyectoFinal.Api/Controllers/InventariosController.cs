using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Data.Models;
using ProyectoFinal.Models.DTOs;

namespace ProyectoFinal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventariosController : ControllerBase
    {
        private readonly ControlInventarioDBContext _ctx;

        public InventariosController(ControlInventarioDBContext ctx)
        {
            _ctx = ctx;
        }

        // GET: api/inventarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventarioDTO>>> GetAll()
        {
            var lista = await _ctx.Inventarios
                .Include(i => i.Producto)
                .OrderBy(i => i.IdInventario)
                .Select(i => new InventarioDTO
                {
                    IdInventario = i.IdInventario,
                    IdProducto = i.IdProducto,
                    NombreProducto = i.Producto.Nombre,
                    Cantidad = i.Cantidad
                })
                .ToListAsync();

            return Ok(lista);
        }

        // GET: api/inventarios/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<InventarioDTO>> GetById(int id)
        {
            var inventario = await _ctx.Inventarios
                .Include(i => i.Producto)
                .Where(i => i.IdInventario == id)
                .Select(i => new InventarioDTO
                {
                    IdInventario = i.IdInventario,
                    IdProducto = i.IdProducto,
                    NombreProducto = i.Producto.Nombre,
                    Cantidad = i.Cantidad
                })
                .FirstOrDefaultAsync();

            if (inventario == null)
                return NotFound();

            return Ok(inventario);
        }

        // POST: api/inventarios
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InventarioDTO dto)
        {
            if (dto == null)
                return BadRequest("Datos requeridos.");

            if (dto.IdProducto <= 0 || dto.Cantidad < 0)
                return BadRequest("Datos inválidos.");

            // Validar producto
            var productoExiste = await _ctx.Productos.AnyAsync(p => p.IdProducto == dto.IdProducto);
            if (!productoExiste)
                return BadRequest("El producto no existe.");

            // ¿Ya existe inventario para ese producto?
            var inventario = await _ctx.Inventarios.FirstOrDefaultAsync(i => i.IdProducto == dto.IdProducto);

            if (inventario == null)
            {
                inventario = new Inventario
                {
                    IdProducto = dto.IdProducto,
                    Cantidad = dto.Cantidad
                };

                _ctx.Inventarios.Add(inventario);
            }
            else
            {
                inventario.Cantidad += dto.Cantidad;
            }

            await _ctx.SaveChangesAsync();
            return Ok(true);
        }

        // PUT: api/inventarios/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] InventarioDTO dto)
        {
            if (dto == null)
                return BadRequest("Datos requeridos.");

            if (dto.IdProducto <= 0 || dto.Cantidad < 0)
                return BadRequest("Datos inválidos.");

            var inventario = await _ctx.Inventarios.FindAsync(id);
            if (inventario == null)
                return NotFound(false);

            // Validar producto
            var productoExiste = await _ctx.Productos.AnyAsync(p => p.IdProducto == dto.IdProducto);
            if (!productoExiste)
                return BadRequest("El producto no existe.");

            inventario.IdProducto = dto.IdProducto;
            inventario.Cantidad = dto.Cantidad;

            await _ctx.SaveChangesAsync();
            return Ok(true);
        }

        // DELETE: api/inventarios/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var inventario = await _ctx.Inventarios.FindAsync(id);
            if (inventario == null)
                return NotFound(false);

            _ctx.Inventarios.Remove(inventario);
            await _ctx.SaveChangesAsync();

            return Ok(true);
        }
    }
}
