using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Core.BusinessLogic;
using ProyectoFinal.Models.DTOs;

namespace ProyectoFinal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GastosController : ControllerBase
    {
        private readonly IGastoBusiness _gastoBusiness;

        public GastosController(IGastoBusiness gastoBusiness)
        {
            _gastoBusiness = gastoBusiness;
        }

        // GET: api/gastos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GastoDTO>>> GetAll()
        {
            var lista = await _gastoBusiness.GetAllAsync();
            return Ok(lista);
        }

        // GET: api/gastos/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GastoDTO>> GetById(int id)
        {
            var dto = await _gastoBusiness.GetByIdAsync(id);
            if (dto is null) return NotFound();
            return Ok(dto);
        }

        // POST: api/gastos
        [HttpPost]
        public async Task<ActionResult<bool>> Create([FromBody] GastoDTO dto)
        {
            if (dto is null) return BadRequest(false);

            var ok = await _gastoBusiness.CreateAsync(dto);
            if (!ok) return BadRequest(false);

            return Ok(true);
        }

        // PUT: api/gastos/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<bool>> Update(int id, [FromBody] GastoDTO dto)
        {
            var ok = await _gastoBusiness.UpdateAsync(id, dto);
            if (!ok) return NotFound(false);

            return Ok(true);
        }

        // DELETE: api/gastos/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var ok = await _gastoBusiness.DeleteAsync(id);
            if (!ok) return NotFound(false);

            return Ok(true);
        }
    }
}