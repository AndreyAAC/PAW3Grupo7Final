using ProyectoFinal.Models.DTOs;
using System.Collections.Generic;

namespace ProyectoFinal.Mvc.Models.Historial
{
    public class HistorialPedidosVM
    {
        public List<PedidoHistorialDTO> Pedidos { get; set; } = new();
    }
}
