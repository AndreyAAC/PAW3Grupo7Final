using ProyectoFinal.Models.DTOs;
using System.Collections.Generic;

namespace ProyectoFinal.Mvc.Models.Inventario
{
    public class InventarioEditVM
    {
        public InventarioDTO Inventario { get; set; } = new();
        public List<ProductoDTO> Productos { get; set; } = new();
    }
}
