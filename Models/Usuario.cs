using System;
using System.Collections.Generic;

namespace Alejandro_Brito.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? NombreCompleto { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Cedula { get; set; } = null!;

    public string Password { get; set; } = null!;

}
