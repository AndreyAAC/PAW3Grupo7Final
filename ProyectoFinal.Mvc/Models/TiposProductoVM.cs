using ProyectoFinal.Models.DTOs;
using System.Collections.Generic;

namespace ProyectoFinal.Mvc.Models.Inventario
{
    public class TiposProductoVM
    {
        public List<TipoProductoDTO> Tipos { get; set; } = new();
    }
}
