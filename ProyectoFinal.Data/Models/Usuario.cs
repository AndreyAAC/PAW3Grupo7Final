using System;
using System.Collections.Generic;

namespace ProyectoFinal.Data.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string NombreApellido { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public int Cedula { get; set; }

    public string? Telefono { get; set; }

    public string Contrasenia { get; set; } = null!;

    public int Role { get; set; }

    public virtual Role? RoleNavigation { get; set; }
}
