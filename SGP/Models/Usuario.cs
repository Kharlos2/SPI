using System;
using System.Collections.Generic;

namespace SGP.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string PrimerApellido { get; set; } = null!;

    public string? SegundoApellido { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int NumeroDoc { get; set; }

    public bool Activo { get; set; }

    public int IdDoc { get; set; }

    public int IdGenero { get; set; }

    public int IdPrograma { get; set; }

    public int IdRol { get; set; }

    public virtual ICollection<Homologacion> Homologacions { get; set; } = new List<Homologacion>();

    public virtual TipoDocumento? IdDocNavigation { get; set; } = null!;

    public virtual Genero? IdGeneroNavigation { get; set; } 

    public virtual Programa? IdProgramaNavigation { get; set; } 

    public virtual Rol? IdRolNavigation { get; set; }

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

}
