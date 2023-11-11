using System;
using System.Collections.Generic;

namespace SGP.Models;

public partial class Asignatura
{
    public int IdAsignatura { get; set; }

    public string Descripcion { get; set; } = null!;

    public string Grupo { get; set; } = null!;

    public int PeriodoAcademico { get; set; }

    public int Aula { get; set; }

    public DateTime Fecha { get; set; }

    public virtual ICollection<Homologacion> Homologacions { get; set; } = new List<Homologacion>();
}
