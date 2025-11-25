using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Data.Models;
using ProyectoFinal.Models.DTOs;

namespace ProyectoFinal.Core.BusinessLogic;

public interface IGastoBusiness
{
    Task<IEnumerable<GastoDTO>> GetAllAsync();
    Task<GastoDTO?> GetByIdAsync(int id);
    Task<bool> CreateAsync(GastoDTO dto);
    Task<bool> UpdateAsync(int id, GastoDTO dto);
    Task<bool> DeleteAsync(int id);
}

public class GastoBusiness : IGastoBusiness
{
    private readonly ControlInventarioDBContext _ctx;

    public GastoBusiness(ControlInventarioDBContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<IEnumerable<GastoDTO>> GetAllAsync()
    {
        var query =
            from g in _ctx.Gastos
            join c in _ctx.CategoriasGasto on g.IdCategoriaGasto equals c.IdCategoriaGasto into cg
            from c in cg.DefaultIfEmpty()
            orderby g.FechaGasto descending, g.IdGasto descending
            select new GastoDTO
            {
                IdGasto = g.IdGasto,
                Motivo = g.Motivo,
                FechaGasto = g.FechaGasto,
                Descripcion = g.Descripcion,
                Monto = g.Monto,
                IdCategoriaGasto = g.IdCategoriaGasto,
                NombreCategoria = c != null ? c.NombreCategoria : null
            };

        return await query.ToListAsync();
    }

    public async Task<GastoDTO?> GetByIdAsync(int id)
    {
        var result = await (
            from g in _ctx.Gastos
            join c in _ctx.CategoriasGasto on g.IdCategoriaGasto equals c.IdCategoriaGasto into cg
            from c in cg.DefaultIfEmpty()
            where g.IdGasto == id
            select new GastoDTO
            {
                IdGasto = g.IdGasto,
                Motivo = g.Motivo,
                FechaGasto = g.FechaGasto,
                Descripcion = g.Descripcion,
                Monto = g.Monto,
                IdCategoriaGasto = g.IdCategoriaGasto,
                NombreCategoria = c != null ? c.NombreCategoria : null
            }).FirstOrDefaultAsync();

        return result;
    }

    public async Task<bool> CreateAsync(GastoDTO dto)
    {
        if (dto is null || string.IsNullOrWhiteSpace(dto.Motivo))
            return false;

        var entity = new Gasto
        {
            Motivo = dto.Motivo,
            FechaGasto = dto.FechaGasto,
            Descripcion = dto.Descripcion,
            Monto = dto.Monto,
            IdCategoriaGasto = dto.IdCategoriaGasto
        };

        _ctx.Gastos.Add(entity);
        await _ctx.SaveChangesAsync();
        dto.IdGasto = entity.IdGasto;

        return true;
    }

    public async Task<bool> UpdateAsync(int id, GastoDTO dto)
    {
        var entity = await _ctx.Gastos.FindAsync(id);
        if (entity == null) return false;

        entity.Motivo = dto.Motivo;
        entity.FechaGasto = dto.FechaGasto;
        entity.Descripcion = dto.Descripcion;
        entity.Monto = dto.Monto;
        entity.IdCategoriaGasto = dto.IdCategoriaGasto;

        await _ctx.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _ctx.Gastos.FindAsync(id);
        if (entity == null) return false;

        _ctx.Gastos.Remove(entity);
        await _ctx.SaveChangesAsync();
        return true;
    }
}