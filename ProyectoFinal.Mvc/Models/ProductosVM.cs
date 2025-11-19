using ProyectoFinal.Models.DTOs;
using System.Collections.Generic;

namespace ProyectoFinal.Mvc.Models.Inventario
{
    public class ProductosVM
    {
        public List<ProductoDTO> Productos { get; set; } = new();
        public List<TipoProductoDTO> TiposProducto { get; set; } = new();
    }
}
