namespace Application.UseCase;

public class OrderRequests (IUnitOfWork unitOfWork) : IOrderRequests
{
   
    public async Task<List<OrderGetRequestDomain>> GetSolicitud(int id)
    {
        var pedido = await unitOfWork.Repository<Pedido>()
                .GetAll()
                .Include(p => p.IdPedidosProductosNavigation) // Incluye productos del pedido
                .ThenInclude(pp => pp.IdPagoNavigation) // Incluye pago dentro del producto
                .Include(p => p.IdCompradorNavigation)
                .ThenInclude(pp => pp.Userprofile) // Cliente// Proveedor
                .Where(u => u.Estado == false && u.IdProveedor == id).ToListAsync()
            ;

        if (!pedido.Any())  throw new Exception("No hay solicitudes");
     

        var pedidosDominio = pedido.Select(p => OrderRequestMapper.ToDomain(p)).ToList();

        return pedidosDominio;

    }
    
    public async Task<int> AceptarSolicitud(int id)
    {
        var pedido = await unitOfWork.Repository<Pedido>().GetByIdAsync(id);

        if (pedido == null)
            throw new Exception("No existe el pedido");

        // Modificar directamente la entidad ya trackeada
        pedido.Estado = true;

        await unitOfWork.SaveChange();
        
        return pedido.IdPedido;
    }

    public async Task<int> VerSiPago(int id)
    {
        var pedido = await unitOfWork.Repository<Pedido>()
            .GetAll()
            .Include(p => p.IdPedidosProductosNavigation)
            .ThenInclude(pp => pp.IdPagoNavigation)
            .FirstOrDefaultAsync(u => u.IdPedido == id );
        ;
            
        if(!(bool)pedido.IdPedidosProductosNavigation.IdPagoNavigation.Estado) throw new Exception("No ha pagado");

        return pedido.IdPedidosProductosNavigation.IdPedidosProductos;
    }
}