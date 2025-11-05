using ProyectoFinal.Models.DTOs;
using System.Collections.Generic;

namespace ProyectoFinal.Mvc.Models.Contabilidad
{
    public class GastosVM
    {
        public List<GastoDTO> Gastos { get; set; } = new();
    }
}