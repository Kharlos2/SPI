using System;
using System.Collections.Generic;

namespace SGP.Models;

public partial class TipoHomologacion
{
    public int IdTipohomologacion { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Homologacion> Homologacions { get; set; } = new List<Homologacion>();
}
