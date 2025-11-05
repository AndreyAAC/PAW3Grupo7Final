using System;
using System.Collections.Generic;

namespace ProyectoFinal.Data.Models;

public partial class Inventario
{
    public int IdInventario { get; set; }

    public int IdProducto { get; set; }

    public int Cantidad { get; set; }

    public virtual Producto Producto { get; set; } = null!;
}
