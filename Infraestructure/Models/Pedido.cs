using System;
using System.Collections.Generic;

namespace Infraestructure.Models;

public partial class Pedido
{
    public int IdPedido { get; set; }

    public int IdProveedor { get; set; }

    public int IdComprador { get; set; }

    public int IdPedidosProductos { get; set; }

    public bool? Estado { get; set; }

    public virtual User IdCompradorNavigation { get; set; } = null!;

    public virtual Pedidosproducto IdPedidosProductosNavigation { get; set; } = null!;

    public virtual User IdProveedorNavigation { get; set; } = null!;
}
