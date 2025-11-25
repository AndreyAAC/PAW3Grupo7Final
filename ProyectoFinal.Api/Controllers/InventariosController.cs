using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Core.BusinessLogic;
using ProyectoFinal.Models.DTOs;

namespace ProyectoFinal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventariosController : ControllerBase
    {
        private readonly IInventarioBusiness _inventarioBusiness;

        public InventariosController(IInventarioBusiness inventarioBusiness)
        {
            _inventarioBusiness = inventarioBusiness;
        }

        // GET: api/inventarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventarioDTO>>> GetAll()
        {
            var lista = await _inventarioBusiness.GetAllAsync();
            return Ok(lista);
        }

        // GET: api/inventarios/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<InventarioDTO>> GetById(int id)
        {
            var dto = await _inventarioBusiness.GetByIdAsync(id);
            if (dto is null)
                return NotFound();

            return Ok(dto);
        }

        // POST: api/inventarios
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InventarioDTO dto)
        {
            if (dto == null)
                return BadRequest("Datos requeridos.");

            var ok = await _inventarioBusiness.CreateAsync(dto);
            if (!ok)
                return BadRequest("Datos inválidos o producto inexistente.");

            return Ok(true);
        }

        // PUT: api/inventarios/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] InventarioDTO dto)
        {
            if (dto == null)
                return BadRequest("Datos requeridos.");

            var ok = await _inventarioBusiness.UpdateAsync(id, dto);
            if (!ok)
                return NotFound(false);

            return Ok(true);
        }

        // DELETE: api/inventarios/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _inventarioBusiness.DeleteAsync(id);
            if (!ok)
                return NotFound(false);

            return Ok(true);
        }
    }
}