using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Data.Models;
using ProyectoFinal.Models.DTOs;

namespace ProyectoFinal.Core.BusinessLogic;

public interface IInventarioBusiness
{
    Task<IEnumerable<InventarioDTO>> GetAllAsync();
    Task<InventarioDTO?> GetByIdAsync(int id);
    Task<bool> CreateAsync(InventarioDTO dto);
    Task<bool> UpdateAsync(int id, InventarioDTO dto);
    Task<bool> DeleteAsync(int id);
}

public class InventarioBusiness : IInventarioBusiness
{
    private readonly ControlInventarioDBContext _ctx;

    public InventarioBusiness(ControlInventarioDBContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<IEnumerable<InventarioDTO>> GetAllAsync()
    {
        return await _ctx.Inventarios
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
    }

    public async Task<InventarioDTO?> GetByIdAsync(int id)
    {
        return await _ctx.Inventarios
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
    }

    public async Task<bool> CreateAsync(InventarioDTO dto)
    {
        if (dto is null || dto.IdProducto <= 0 || dto.Cantidad < 0)
            return false;

        var productoExiste = await _ctx.Productos.AnyAsync(p => p.IdProducto == dto.IdProducto);
        if (!productoExiste)
            return false;

        var inventario = await _ctx.Inventarios
            .FirstOrDefaultAsync(i => i.IdProducto == dto.IdProducto);

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
        dto.IdInventario = inventario.IdInventario;

        return true;
    }

    public async Task<bool> UpdateAsync(int id, InventarioDTO dto)
    {
        if (dto is null || dto.IdProducto <= 0 || dto.Cantidad < 0)
            return false;

        var inventario = await _ctx.Inventarios.FindAsync(id);
        if (inventario == null)
            return false;

        var productoExiste = await _ctx.Productos.AnyAsync(p => p.IdProducto == dto.IdProducto);
        if (!productoExiste)
            return false;

        inventario.IdProducto = dto.IdProducto;
        inventario.Cantidad = dto.Cantidad;

        await _ctx.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var inventario = await _ctx.Inventarios.FindAsync(id);
        if (inventario == null)
            return false;

        _ctx.Inventarios.Remove(inventario);
        await _ctx.SaveChangesAsync();

        return true;
    }
}