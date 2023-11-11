using System;
using System.Collections.Generic;

namespace SGP.Models;

public partial class Genero
{
    public int IdGenero { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
