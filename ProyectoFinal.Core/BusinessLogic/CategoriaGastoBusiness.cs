using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Data.Models;
using ProyectoFinal.Models.DTOs;

namespace ProyectoFinal.Core.BusinessLogic;

public interface ICategoriaGastoBusiness
{
    Task<IEnumerable<CategoriaGastoDTO>> GetAllAsync();
}

public class CategoriaGastoBusiness : ICategoriaGastoBusiness
{
    private readonly ControlInventarioDBContext _ctx;

    public CategoriaGastoBusiness(ControlInventarioDBContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<IEnumerable<CategoriaGastoDTO>> GetAllAsync()
    {
        return await _ctx.CategoriasGasto
            .OrderBy(c => c.NombreCategoria)
            .Select(c => new CategoriaGastoDTO
            {
                IdCategoriaGasto = c.IdCategoriaGasto,
                NombreCategoria = c.NombreCategoria
            })
            .ToListAsync();
    }
}