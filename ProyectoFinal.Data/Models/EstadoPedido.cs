using System;
using System.Collections.Generic;

namespace ProyectoFinal.Data.Models;

public partial class EstadoPedido
{
    public int IdEstadoPedido { get; set; }

    public string NombreEstado { get; set; } = null!;

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
