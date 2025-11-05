using System;
using System.Collections.Generic;

namespace ProyectoFinal.Data.Models;

public partial class CuentasPagar
{
    public int IdCuentaPagar { get; set; }

    public string Motivo { get; set; } = null!;

    public DateOnly FechaCuentaPagar { get; set; }

    public string? Descripcion { get; set; }

    public decimal Monto { get; set; }

    public string? PlazoPagar { get; set; }
}
