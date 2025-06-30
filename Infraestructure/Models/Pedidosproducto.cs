using System;
using System.Collections.Generic;

namespace Infraestructure.Models;

public partial class Pedidosproducto
{
    public int IdPedidosProductos { get; set; }

    public string Producto { get; set; } = null!;

    public int Cantidad { get; set; }

    public string Descripcion { get; set; } = null!;

    public string DireccionEntrega { get; set; } = null!;

    public DateTime FechaLlegadaAcordada { get; set; }

    public DateTime? FechaSolicitada { get; set; }

    public string NombreTransaccion { get; set; } = null!;

    public int? IdPago { get; set; }

    public int? IdPreparacion { get; set; }

    public virtual Pago? IdPagoNavigation { get; set; }

    public virtual Preparacion? IdPreparacionNavigation { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
