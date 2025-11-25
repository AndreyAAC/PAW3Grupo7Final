using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Core.BusinessLogic;
using ProyectoFinal.Models.DTOs;

namespace ProyectoFinal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoBusiness _productoBusiness;

        public ProductosController(IProductoBusiness productoBusiness)
        {
            _productoBusiness = productoBusiness;
        }

        // GET: api/productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> GetAll()
        {
            var lista = await _productoBusiness.GetAllAsync();
            return Ok(lista);
        }

        // GET: api/productos/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductoDTO>> GetById(int id)
        {
            var dto = await _productoBusiness.GetByIdAsync(id);
            if (dto == null) return NotFound();

            return Ok(dto);
        }

        // POST: api/productos
        [HttpPost]
        public async Task<ActionResult<bool>> Create([FromBody] ProductoDTO dto)
        {
            if (dto is null) return BadRequest(false);

            var ok = await _productoBusiness.CreateAsync(dto);
            if (!ok) return BadRequest(false);

            return Ok(true);
        }

        // PUT: api/productos/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<bool>> Update(int id, [FromBody] ProductoDTO dto)
        {
            var ok = await _productoBusiness.UpdateAsync(id, dto);
            if (!ok) return NotFound(false);

            return Ok(true);
        }

        // DELETE: api/productos/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var ok = await _productoBusiness.DeleteAsync(id);
            if (!ok) return NotFound(false);

            return Ok(true);
        }
    }
}