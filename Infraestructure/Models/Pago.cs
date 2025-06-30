using System;
using System.Collections.Generic;

namespace Infraestructure.Models;

public partial class Pago
{
    public int IdPago { get; set; }

    public DateTime? FechaPago { get; set; }

    public bool? Estado { get; set; }

    public decimal? Monto { get; set; }

    public int? IdBoleta { get; set; }

    public virtual Boletum? IdBoletaNavigation { get; set; }

    public virtual ICollection<Pedidosproducto> Pedidosproductos { get; set; } = new List<Pedidosproducto>();
}
