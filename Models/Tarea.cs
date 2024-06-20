using System;
using System.Collections.Generic;

namespace Alejandro_Brito.Models;

public partial class Tarea
{
    public int IdTarea { get; set; }

    public string Titulo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public DateOnly? FechaCreacion { get; set; }

    public DateOnly? FechaVencimiento { get; set; }

    public int? Estado { get; set; }

    public virtual Estado? oEstado { get; set; }
}
