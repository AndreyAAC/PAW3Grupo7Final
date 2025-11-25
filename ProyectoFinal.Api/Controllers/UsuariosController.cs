using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Data.Models;
using ProyectoFinal.Models.DTOs;

namespace ProyectoFinal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly ControlInventarioDBContext _ctx;

        public UsuariosController(ControlInventarioDBContext ctx)
        {
            _ctx = ctx;
        }

        // GET: api/usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioAdminDTO>>> GetAll()
        {
            var query = from u in _ctx.Usuarios
                        join r in _ctx.Roles on u.Role equals r.IdRole into ur
                        from r in ur.DefaultIfEmpty()
                        orderby u.NombreApellido
                        select new UsuarioAdminDTO
                        {
                            IdUsuario = u.IdUsuario,
                            NombreUsuario = u.NombreUsuario,
                            NombreApellido = u.NombreApellido,
                            Correo = u.Correo,
                            Cedula = u.Cedula,
                            Telefono = u.Telefono,
                            Role = u.Role,
                            NombreRole = r != null ? r.NombreRole : null
                        };

            var lista = await query.ToListAsync();
            return Ok(lista);
        }

        // GET: api/usuarios/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<UsuarioAdminDTO>> GetById(int id)
        {
            var query = from u in _ctx.Usuarios
                        join r in _ctx.Roles on u.Role equals r.IdRole into ur
                        from r in ur.DefaultIfEmpty()
                        where u.IdUsuario == id
                        select new UsuarioAdminDTO
                        {
                            IdUsuario = u.IdUsuario,
                            NombreUsuario = u.NombreUsuario,
                            NombreApellido = u.NombreApellido,
                            Correo = u.Correo,
                            Cedula = u.Cedula,
                            Telefono = u.Telefono,
                            Role = u.Role,
                            NombreRole = r != null ? r.NombreRole : null
                        };

            var dto = await query.FirstOrDefaultAsync();
            if (dto == null) return NotFound();

            return Ok(dto);
        }

        // PUT: api/usuarios/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<bool>> Update(int id, [FromBody] UsuarioAdminDTO dto)
        {
            var entity = await _ctx.Usuarios.FindAsync(id);
            if (entity == null) return NotFound(false);

            entity.NombreUsuario = dto.NombreUsuario;
            entity.NombreApellido = dto.NombreApellido;
            entity.Correo = dto.Correo;
            entity.Cedula = dto.Cedula;
            entity.Telefono = dto.Telefono;
            entity.Role = dto.Role;

            await _ctx.SaveChangesAsync();
            return Ok(true);
        }
    }
}