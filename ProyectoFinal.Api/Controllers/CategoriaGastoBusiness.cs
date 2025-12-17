using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Core.BusinessLogic;
using ProyectoFinal.Models.DTOs;

namespace ProyectoFinal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasGastoController : ControllerBase
    {
        private readonly ICategoriaGastoBusiness _business;

        public CategoriasGastoController(ICategoriaGastoBusiness business)
        {
            _business = business;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaGastoDTO>>> GetAll()
        {
            var data = await _business.GetAllAsync();
            return Ok(data);
        }
    }
}