using System;
using System.Collections.Generic;

namespace ProyectoFinal.Data.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Imagen { get; set; }

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public int? IdTipoProducto { get; set; }
    public virtual TipoProducto? TipoProducto { get; set; }
    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
    public virtual ICollection<PedidoDetalle> PedidoDetalles { get; set; } = new List<PedidoDetalle>();
    public virtual ICollection<Cita> Citas { get; set; } = new List<Cita>();
}
