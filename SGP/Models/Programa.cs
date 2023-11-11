using System;
using System.Collections.Generic;

namespace SGP.Models;

public partial class Programa
{
    public int IdPrograma { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Programacion> Programacions { get; set; } = new List<Programacion>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
