using System;
using System.Collections.Generic;

namespace ProyectoFinal.Data.Models;

public partial class CategoriaGasto
{
    public int IdCategoriaGasto { get; set; }

    public string NombreCategoria { get; set; } = null!;

    public virtual ICollection<Gasto> Gastos { get; set; } = new List<Gasto>();
}
