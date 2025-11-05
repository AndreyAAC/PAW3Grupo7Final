using System;
using System.Collections.Generic;

namespace ProyectoFinal.Data.Models;

public partial class Cita
{
    public int IdCita { get; set; }

    public int IdCliente { get; set; }

    public string Motivo { get; set; } = null!;

    public int? Producto { get; set; }

    public string? Detalle { get; set; }

    public DateOnly FechaCita { get; set; }

    public TimeOnly HoraCita { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;
    public virtual Producto? ProductoNavigation { get; set; }
}