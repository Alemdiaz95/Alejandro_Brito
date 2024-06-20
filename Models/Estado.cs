using System;
using System.Collections.Generic;

namespace Alejandro_Brito.Models;

public partial class Estado
{
    public int IdEstado { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
}
