using System;
using System.Collections.Generic;

namespace Infraestructure.Models;

public partial class Boletum
{
    public int IdBoleta { get; set; }

    public string Ruta { get; set; } = null!;

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
