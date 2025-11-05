using System;
using System.Collections.Generic;

namespace ProyectoFinal.Data.Models;

public partial class TipoProducto
{
    public int IdTipoProducto { get; set; }

    public string NombreTipo { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
