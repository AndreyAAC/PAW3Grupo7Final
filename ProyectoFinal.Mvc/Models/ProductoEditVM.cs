using ProyectoFinal.Models.DTOs;
using System.Collections.Generic;

namespace ProyectoFinal.Mvc.Models.Inventario
{
    public class ProductoEditVM
    {
        public ProductoDTO Producto { get; set; } = new();
        public List<TipoProductoDTO> TiposProducto { get; set; } = new();
    }
}
