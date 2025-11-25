using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Core.BusinessLogic;
using ProyectoFinal.Models.DTOs;

namespace ProyectoFinal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TiposProductoController : ControllerBase
    {
        private readonly ITipoProductoBusiness _tipoProductoBusiness;

        public TiposProductoController(ITipoProductoBusiness tipoProductoBusiness)
        {
            _tipoProductoBusiness = tipoProductoBusiness;
        }

        // GET: api/TiposProducto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoProductoDTO>>> GetAll()
        {
            var lista = await _tipoProductoBusiness.GetAllAsync();
            return Ok(lista);
        }

        // GET: api/TiposProducto/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TipoProductoDTO>> GetById(int id)
        {
            var dto = await _tipoProductoBusiness.GetByIdAsync(id);
            if (dto == null) return NotFound();

            return Ok(dto);
        }

        // POST: api/TiposProducto
        [HttpPost]
        public async Task<ActionResult<bool>> Create([FromBody] TipoProductoDTO dto)
        {
            if (dto is null) return BadRequest(false);

            var ok = await _tipoProductoBusiness.CreateAsync(dto);
            if (!ok) return BadRequest(false);

            return Ok(true);
        }

        // PUT: api/TiposProducto/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<bool>> Update(int id, [FromBody] TipoProductoDTO dto)
        {
            var ok = await _tipoProductoBusiness.UpdateAsync(id, dto);
            if (!ok) return NotFound(false);

            return Ok(true);
        }

        // DELETE: api/TiposProducto/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var ok = await _tipoProductoBusiness.DeleteAsync(id);
            if (!ok) return NotFound(false);

            return Ok(true);
        }
    }
}