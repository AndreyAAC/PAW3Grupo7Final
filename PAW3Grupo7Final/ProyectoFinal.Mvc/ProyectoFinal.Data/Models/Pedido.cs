using System;
using System.Collections.Generic;

namespace ProyectoFinal.Data.Models;

public partial class Pedido
{
    public int IdPedido { get; set; }

    public DateOnly FechaDeInicio { get; set; }

    public DateOnly? FechaDeEntrega { get; set; }

    public int IdEstadoPedido { get; set; }

    public virtual EstadoPedido EstadoPedido { get; set; } = null!;
    public virtual ICollection<PedidoDetalle> PedidoDetalles { get; set; } = new List<PedidoDetalle>();

}
