using System;
using System.Collections.Generic;

namespace Infraestructure.Models;

public partial class Envio
{
    public int IdEnvio { get; set; }

    public bool? Estado { get; set; }

    public bool? Llegada { get; set; }

    public string NombreEmpresa { get; set; } = null!;

    public string RucEmpresa { get; set; } = null!;

    public string Asesor { get; set; } = null!;

    public string NumeroTelefonico { get; set; } = null!;

    public string DireccionEnvio { get; set; } = null!;

    public string DireccionRecojo { get; set; } = null!;

    public DateTime FechaLlegada { get; set; }

    public string NroGuia { get; set; } = null!;

    public virtual ICollection<Preparacion> Preparacions { get; set; } = new List<Preparacion>();
}
