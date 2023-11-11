using System;
using System.Collections.Generic;

namespace SGP.Models;

public partial class Programacion
{
    public int IdProgramacion { get; set; }

    public string Periodo { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public string Aula { get; set; } = null!;

    public string Asignatura { get; set; } = null!;

    public string Grupo { get; set; } = null!;

    public int IdPrograma { get; set; }

    public virtual Programa IdProgramaNavigation { get; set; } = null!;
}
