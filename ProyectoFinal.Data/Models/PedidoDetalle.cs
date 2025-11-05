using System;
using System.Collections.Generic;

namespace ProyectoFinal.Data.Models;

public partial class PedidoDetalle
{
    public int IdPedido { get; set; }

    public int IdProducto { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public virtual Pedido Pedido { get; set; } = null!;
    public virtual Producto Producto { get; set; } = null!;
}
