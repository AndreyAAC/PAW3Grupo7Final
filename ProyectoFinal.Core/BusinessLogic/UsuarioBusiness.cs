using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Data.Models;
using ProyectoFinal.Models.DTOs;

namespace ProyectoFinal.Core.BusinessLogic;

public interface IUsuarioBusiness
{
    Task<bool> RegisterAsync(UsuarioRegisterDTO dto);
    Task<UsuarioLoginResultDTO?> LoginAsync(UsuarioLoginDTO dto);
}

public class UsuarioBusiness : IUsuarioBusiness
{
    private readonly ControlInventarioDBContext _ctx;

    public UsuarioBusiness(ControlInventarioDBContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<bool> RegisterAsync(UsuarioRegisterDTO dto)
    {
        if (dto is null) return false;
        if (dto.Contrasenia != dto.ConfirmarContrasenia) return false;
        if (string.IsNullOrWhiteSpace(dto.Nombre) ||
            string.IsNullOrWhiteSpace(dto.Apellido) ||
            string.IsNullOrWhiteSpace(dto.Correo) ||
            string.IsNullOrWhiteSpace(dto.Contrasenia))
        {
            return false;
        }

        var emailExists = await _ctx.Usuarios.AnyAsync(u => u.Correo == dto.Correo);
        if (emailExists) return false;

        var cedulaExists = await _ctx.Usuarios.AnyAsync(u => u.Cedula == dto.Cedula);
        if (cedulaExists) return false;

        var usuario = new Usuario
        {
            NombreUsuario = dto.Nombre,
            NombreApellido = $"{dto.Nombre} {dto.Apellido}",
            Correo = dto.Correo,
            Cedula = dto.Cedula,
            Telefono = dto.Telefono,
            Contrasenia = dto.Contrasenia,
            Role = 1
        };

        _ctx.Usuarios.Add(usuario);
        await _ctx.SaveChangesAsync();

        return true;
    }

    public async Task<UsuarioLoginResultDTO?> LoginAsync(UsuarioLoginDTO dto)
    {
        var user = await _ctx.Usuarios
            .FirstOrDefaultAsync(u => u.Correo == dto.Correo && u.Contrasenia == dto.Contrasenia);

        if (user == null) return null;

        return new UsuarioLoginResultDTO
        {
            IdUsuario = user.IdUsuario,
            Nombre = user.NombreUsuario,
            Correo = user.Correo,
            RoleId = user.Role
        };
    }
}