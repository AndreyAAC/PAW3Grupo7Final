using ProyectoFinal.Models.DTOs;
using System.Collections.Generic;

namespace ProyectoFinal.Mvc.Models.Historial
{
    public class HistorialCitasVM
    {
        public List<CitaHistorialDTO> Citas { get; set; } = new();
    }
}
