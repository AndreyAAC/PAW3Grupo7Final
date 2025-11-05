using System;
using System.Collections.Generic;

namespace ProyectoFinal.Data.Models;

public partial class Gasto
{
    public int IdGasto { get; set; }

    public string Motivo { get; set; } = null!;

    public DateOnly FechaGasto { get; set; }

    public string? Descripcion { get; set; }

    public decimal Monto { get; set; }

    public int? IdCategoriaGasto { get; set; }

    public virtual CategoriaGasto? CategoriaGasto { get; set; }
}
