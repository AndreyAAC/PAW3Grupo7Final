using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Core.BusinessLogic;
using ProyectoFinal.Models.DTOs;

namespace ProyectoFinal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioBusiness _usuarioBusiness;

        public AuthController(IUsuarioBusiness usuarioBusiness)
        {
            _usuarioBusiness = usuarioBusiness;
        }

        // POST: api/Auth/register
        [HttpPost("register")]
        public async Task<ActionResult<bool>> Register([FromBody] UsuarioRegisterDTO dto)
        {
            var ok = await _usuarioBusiness.RegisterAsync(dto);
            if (!ok) return BadRequest(false);
            return Ok(true);
        }

        // POST: api/Auth/login
        [HttpPost("login")]
        public async Task<ActionResult<UsuarioLoginResultDTO>> Login([FromBody] UsuarioLoginDTO dto)
        {
            var user = await _usuarioBusiness.LoginAsync(dto);
            if (user == null) return Unauthorized();

            return Ok(user);
        }
    }
}