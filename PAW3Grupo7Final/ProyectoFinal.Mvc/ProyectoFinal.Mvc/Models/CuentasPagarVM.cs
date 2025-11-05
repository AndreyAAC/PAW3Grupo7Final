using ProyectoFinal.Models.DTOs;
using System.Collections.Generic;

namespace ProyectoFinal.Mvc.Models.Contabilidad
{
    public class CuentasPagarVM
    {
        public List<CuentaPagarDTO> Cuentas { get; set; } = new();
    }
}