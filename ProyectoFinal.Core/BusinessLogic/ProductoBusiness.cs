using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Data.Models;
using ProyectoFinal.Models.DTOs;

namespace ProyectoFinal.Core.BusinessLogic;

public interface IProductoBusiness
{
    Task<IEnumerable<ProductoDTO>> GetAllAsync();
    Task<ProductoDTO?> GetByIdAsync(int id);
    Task<bool> CreateAsync(ProductoDTO dto);
    Task<bool> UpdateAsync(int id, ProductoDTO dto);
    Task<bool> DeleteAsync(int id);
}

public class ProductoBusiness : IProductoBusiness
{
    private readonly ControlInventarioDBContext _ctx;

    public ProductoBusiness(ControlInventarioDBContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<IEnumerable<ProductoDTO>> GetAllAsync()
    {
        var query =
            from p in _ctx.Productos
            join t in _ctx.TiposProducto on p.IdTipoProducto equals t.IdTipoProducto into pt
            from t in pt.DefaultIfEmpty()
            orderby p.Nombre
            select new ProductoDTO
            {
                IdProducto = p.IdProducto,
                Nombre = p.Nombre,
                Imagen = p.Imagen,
                Descripcion = p.Descripcion,
                Precio = p.Precio,
                IdTipoProducto = p.IdTipoProducto,
                NombreTipoProducto = t != null ? t.NombreTipo : null
            };

        return await query.ToListAsync();
    }

    public async Task<ProductoDTO?> GetByIdAsync(int id)
    {
        var query =
            from p in _ctx.Productos
            join t in _ctx.TiposProducto on p.IdTipoProducto equals t.IdTipoProducto into pt
            from t in pt.DefaultIfEmpty()
            where p.IdProducto == id
            select new ProductoDTO
            {
                IdProducto = p.IdProducto,
                Nombre = p.Nombre,
                Imagen = p.Imagen,
                Descripcion = p.Descripcion,
                Precio = p.Precio,
                IdTipoProducto = p.IdTipoProducto,
                NombreTipoProducto = t != null ? t.NombreTipo : null
            };

        return await query.FirstOrDefaultAsync();
    }

    public async Task<bool> CreateAsync(ProductoDTO dto)
    {
        if (dto is null || string.IsNullOrWhiteSpace(dto.Nombre))
            return false;

        var entity = new Producto
        {
            Nombre = dto.Nombre,
            Imagen = dto.Imagen,
            Descripcion = dto.Descripcion,
            Precio = dto.Precio,
            IdTipoProducto = dto.IdTipoProducto
        };

        _ctx.Productos.Add(entity);
        await _ctx.SaveChangesAsync();
        dto.IdProducto = entity.IdProducto;

        return true;
    }

    public async Task<bool> UpdateAsync(int id, ProductoDTO dto)
    {
        var entity = await _ctx.Productos.FindAsync(id);
        if (entity == null) return false;

        entity.Nombre = dto.Nombre;
        entity.Imagen = dto.Imagen;
        entity.Descripcion = dto.Descripcion;
        entity.Precio = dto.Precio;
        entity.IdTipoProducto = dto.IdTipoProducto;

        await _ctx.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _ctx.Productos.FindAsync(id);
        if (entity == null) return false;

        _ctx.Productos.Remove(entity);
        await _ctx.SaveChangesAsync();
        return true;
    }
}