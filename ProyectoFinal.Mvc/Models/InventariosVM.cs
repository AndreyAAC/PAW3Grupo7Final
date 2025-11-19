using ProyectoFinal.Models.DTOs;
using System.Collections.Generic;

namespace ProyectoFinal.Mvc.Models.Inventario
{
    public class InventariosVM
    {
        public List<InventarioDTO> Inventarios { get; set; } = new();
        public List<ProductoDTO> Productos { get; set; } = new();
    }
}
