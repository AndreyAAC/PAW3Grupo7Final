using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Data.Models;
using ProyectoFinal.Models.DTOs;

namespace ProyectoFinal.Core.BusinessLogic;

public interface ITipoProductoBusiness
{
    Task<IEnumerable<TipoProductoDTO>> GetAllAsync();
    Task<TipoProductoDTO?> GetByIdAsync(int id);
    Task<bool> CreateAsync(TipoProductoDTO dto);
    Task<bool> UpdateAsync(int id, TipoProductoDTO dto);
    Task<bool> DeleteAsync(int id);
}

public class TipoProductoBusiness : ITipoProductoBusiness
{
    private readonly ControlInventarioDBContext _ctx;

    public TipoProductoBusiness(ControlInventarioDBContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<IEnumerable<TipoProductoDTO>> GetAllAsync()
    {
        return await _ctx.TiposProducto
            .OrderBy(t => t.NombreTipo)
            .Select(t => new TipoProductoDTO
            {
                IdTipoProducto = t.IdTipoProducto,
                NombreTipo = t.NombreTipo
            })
            .ToListAsync();
    }

    public async Task<TipoProductoDTO?> GetByIdAsync(int id)
    {
        return await _ctx.TiposProducto
            .Where(t => t.IdTipoProducto == id)
            .Select(t => new TipoProductoDTO
            {
                IdTipoProducto = t.IdTipoProducto,
                NombreTipo = t.NombreTipo
            })
            .FirstOrDefaultAsync();
    }

    public async Task<bool> CreateAsync(TipoProductoDTO dto)
    {
        if (dto is null || string.IsNullOrWhiteSpace(dto.NombreTipo))
            return false;

        var entity = new TipoProducto
        {
            NombreTipo = dto.NombreTipo
        };

        _ctx.TiposProducto.Add(entity);
        await _ctx.SaveChangesAsync();
        dto.IdTipoProducto = entity.IdTipoProducto;

        return true;
    }

    public async Task<bool> UpdateAsync(int id, TipoProductoDTO dto)
    {
        var entity = await _ctx.TiposProducto.FindAsync(id);
        if (entity == null) return false;

        if (string.IsNullOrWhiteSpace(dto.NombreTipo))
            return false;

        entity.NombreTipo = dto.NombreTipo;
        await _ctx.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _ctx.TiposProducto.FindAsync(id);
        if (entity == null) return false;

        _ctx.TiposProducto.Remove(entity);
        await _ctx.SaveChangesAsync();
        return true;
    }
}