using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Data.Models;
using ProyectoFinal.Models.DTOs;

namespace ProyectoFinal.Core.BusinessLogic;

public interface ICuentaPagarBusiness
{
    Task<IEnumerable<CuentaPagarDTO>> GetAllAsync();
    Task<CuentaPagarDTO?> GetByIdAsync(int id);
    Task<bool> CreateAsync(CuentaPagarDTO dto);
    Task<bool> UpdateAsync(int id, CuentaPagarDTO dto);
    Task<bool> DeleteAsync(int id);
}

public class CuentaPagarBusiness : ICuentaPagarBusiness
{
    private readonly ControlInventarioDBContext _ctx;

    public CuentaPagarBusiness(ControlInventarioDBContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<IEnumerable<CuentaPagarDTO>> GetAllAsync()
    {
        return await _ctx.CuentasPorPagar
            .OrderByDescending(c => c.FechaCuentaPagar)
            .ThenByDescending(c => c.IdCuentaPagar)
            .Select(c => new CuentaPagarDTO
            {
                IdCuentaPagar = c.IdCuentaPagar,
                Motivo = c.Motivo,
                FechaCuentaPagar = c.FechaCuentaPagar,
                Descripcion = c.Descripcion,
                Monto = c.Monto,
                PlazoPagar = c.PlazoPagar
            })
            .ToListAsync();
    }

    public async Task<CuentaPagarDTO?> GetByIdAsync(int id)
    {
        return await _ctx.CuentasPorPagar
            .Where(c => c.IdCuentaPagar == id)
            .Select(c => new CuentaPagarDTO
            {
                IdCuentaPagar = c.IdCuentaPagar,
                Motivo = c.Motivo,
                FechaCuentaPagar = c.FechaCuentaPagar,
                Descripcion = c.Descripcion,
                Monto = c.Monto,
                PlazoPagar = c.PlazoPagar
            })
            .FirstOrDefaultAsync();
    }

    public async Task<bool> CreateAsync(CuentaPagarDTO dto)
    {
        if (dto is null || string.IsNullOrWhiteSpace(dto.Motivo))
            return false;

        var entity = new CuentasPagar
        {
            Motivo = dto.Motivo,
            FechaCuentaPagar = dto.FechaCuentaPagar,
            Descripcion = dto.Descripcion,
            Monto = dto.Monto,
            PlazoPagar = dto.PlazoPagar
        };

        _ctx.CuentasPorPagar.Add(entity);
        await _ctx.SaveChangesAsync();
        dto.IdCuentaPagar = entity.IdCuentaPagar;

        return true;
    }

    public async Task<bool> UpdateAsync(int id, CuentaPagarDTO dto)
    {
        var entity = await _ctx.CuentasPorPagar.FindAsync(id);
        if (entity == null) return false;

        entity.Motivo = dto.Motivo;
        entity.FechaCuentaPagar = dto.FechaCuentaPagar;
        entity.Descripcion = dto.Descripcion;
        entity.Monto = dto.Monto;
        entity.PlazoPagar = dto.PlazoPagar;

        await _ctx.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _ctx.CuentasPorPagar.FindAsync(id);
        if (entity == null) return false;

        _ctx.CuentasPorPagar.Remove(entity);
        await _ctx.SaveChangesAsync();
        return true;
    }
}