using System;
using System.Collections.Generic;

namespace ProyectoFinal.Data.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public virtual ICollection<Cita> Citas { get; set; } = new List<Cita>();
}
