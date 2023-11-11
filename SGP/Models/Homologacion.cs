using System;
using System.Collections.Generic;

namespace SGP.Models;

public partial class Homologacion
{
    public int IdHomologacion { get; set; }

    public string Descripcion { get; set; } = null!;

    public int IdTipohomologacion { get; set; }

    public int IdUsuario { get; set; }

    public int IdAsignatura { get; set; }

    public string TipoDocumento { get; set; } = null!;

    public int NumeroDoc { get; set; }

    public int NivelAnterior { get; set; }

    public string AsignaturaAnterior { get; set; } = null!;

    public int CreditoAnterior { get; set; }

    public int NivelNuevo { get; set; }

    public string Programa { get; set; } = null!;

    public string AsignaturaNueva { get; set; } = null!;

    public int CreditoNuevo { get; set; }

    public int Nota { get; set; }

    public virtual Asignatura IdAsignaturaNavigation { get; set; } = null!;

    public virtual TipoHomologacion IdTipohomologacionNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
