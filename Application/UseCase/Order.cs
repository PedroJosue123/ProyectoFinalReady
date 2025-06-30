using Application.IUseCase;
using Application.Mappers;
using Domain.Dtos;
using Domain.Entities;
using Domain.Interface;
using Infraestructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCase;

public class Order  (IUnitOfWork unitOfWork)  : IOrder
{
    public async Task<int> RegisterOrder(RegisterOrderRequestDto requestDto, int id)
    {
        
        var usuariosEncontrados = await unitOfWork.Repository<User>()
            .GetAll()
            .CountAsync(u => u.UserId == id || u.UserId == requestDto.Idproveedor);

        if (usuariosEncontrados < 2)
            throw new Exception("Uno o ambos usuarios no existen");

       
        var order = new OrderDomain(requestDto.Idproveedor, id, 0, false);

        var orderef = OrderMapper.ToEntity(order);

        var detalles_producto = new ProductOrdenDomain( 0, requestDto.Producto, requestDto.Cantidad , requestDto.Descripcion, 
            requestDto.DireccionEntrega, requestDto.FechaLlegadaAcordada, requestDto.NombreTransaccion , 0);

        var detalleef = ProductMapper.ToEntity(detalles_producto);
        detalleef.FechaSolicitada =  DateTime.Now;
        var pago = new PaymentsDomain(0, null,  false, requestDto.Monto  );
        var pagoef = PaymentMapper.ToEntity(pago);
        
        try
        {
            await unitOfWork.BeginTransactionAsync();

            // Guardamos primero el usuario
            await unitOfWork.Repository<Pago>().AddAsync(pagoef);
            await unitOfWork.SaveChange();
            
            detalleef.IdPago = pagoef.IdPago;
            
            await unitOfWork.Repository<Pedidosproducto>().AddAsync(detalleef);
            await unitOfWork.SaveChange();

            // Asociamos el ID generado al perfil
            orderef.IdPedidosProductos = detalleef.IdPedidosProductos;

            // Guardamos el perfil
            await unitOfWork.Repository<Pedido>().AddAsync(orderef);
            await unitOfWork.SaveChange();
            
            
            await unitOfWork.CommitTransactionAsync();
            return orderef.IdPedido;
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync();
            throw new Exception("Datos Incompletos");

        }
        
    }

    public async Task<List<GetListPrepationDomain>> ListGetPreparationOrder(int id)
    {
       
           
        var pedido = await unitOfWork.Repository<Pedido>()
            .GetAll()
            .Where(u => u.IdProveedor == id && u.IdPedidosProductosNavigation.IdPagoNavigation.Estado == true)
            .Include(p => p.IdCompradorNavigation.Userprofile)
            .Include(p => p.IdPedidosProductosNavigation)
            .Include(p => p.IdPedidosProductosNavigation.IdPreparacionNavigation)
            .ThenInclude(pp=> pp.IdEnvioNavigation)
            .ToListAsync();
        
        if (!pedido.Any()) throw new Exception("No hay ni mierda");
        var pedidosDominio = pedido.Select(p => GetListPreparationMapper.ToDomain(p)).ToList();

        return pedidosDominio;
    }
    
    public async Task<GetSellerOrderDomain> GetProveedorPreparationOrder(int idPedidosProducto)
    {


        var pedido = await unitOfWork.Repository<Pedido>()
            .GetAll()
            .Where(u => u.IdPedidosProductos == idPedidosProducto)
            .Include(p => p.IdCompradorNavigation.Userprofile)
            .Include(p => p.IdPedidosProductosNavigation)
            .ThenInclude(pp => pp.IdPagoNavigation).FirstAsync();
           
        
        if (pedido == null) throw new Exception("No hay ni mierda");
        var pedidosDominio = GetSellerOrderMapper.ToDomain(pedido);
        return pedidosDominio;
    }
    
    

    public async Task<int> PreparetedOrder(int id, PreparationOrderDto preparationOrderDto)
    {

        var preparacion = new PreparationOrderDomain(0 ,preparationOrderDto.ComoEnvia ,preparationOrderDto.Detalles);

        var preparacionEntity = PreparationOrderMapper.ToEntity(preparacion);
        await unitOfWork.Repository<Preparacion>().AddAsync(preparacionEntity);
        
        await unitOfWork.SaveChange();
    
        var pediproducto =  await unitOfWork.Repository<Pedidosproducto>().GetByIdAsync(id);
        
        

        pediproducto.IdPreparacion = preparacionEntity.IdPreparacion;

        await unitOfWork.SaveChange();

        return (int)pediproducto.IdPreparacion;

    }

    public async Task<bool> VerSiOrderAceptado(int id)
    {
        var pedido = await unitOfWork.Repository<Pedido>().GetByIdAsync(id);
        
        if ((bool)!pedido.Estado) throw new Exception("No fue aceptado");

        return true;

    }

    public async Task<List<GetOrderDomain>> MostrarOrder(int id)
    {


        var pedido = await unitOfWork.Repository<Pedido>()
            .GetAll().Include(p => p.IdPedidosProductosNavigation) // Incluye productos del pedido
            .ThenInclude(pp => pp.IdPagoNavigation) // Incluye pago dentro del producto
            .Include(p => p.IdProveedorNavigation)
            .ThenInclude(pp => pp.Userprofile) // Cliente// Proveedor
            .Where(u => u.IdComprador == id).ToListAsync();
            
        if (!pedido.Any()) throw new Exception("No hay ni mierda");
        var pedidosDominio = pedido.Select(p => GetOrderMapper.ToDomain(p)).ToList();

        return pedidosDominio;


    }



    public async Task<GetPreparationOrderDomain>  MostrarPedidosPreparados(int idComprador, int idPedido)
    {
        var pedidosPreparados = await unitOfWork.Repository<Pedido>()
            .GetAll().Where(u => u.IdComprador == idComprador && u.IdPedido == idPedido )
            .Include(p => p.IdPedidosProductosNavigation.IdPreparacionNavigation.IdEnvioNavigation).FirstAsync();
        
        if (pedidosPreparados == null) throw new Exception("No hay nada");
        var pedidosDominio = GetPreparationOrderMapper.ToDomain(pedidosPreparados);

        return pedidosDominio;
    }
    
    public async Task<List<GetListPreparationOrderDomain>>  ListaMostrarPedidosPreparados(int idComprador)
    {
        var ListapedidosPreparados = await unitOfWork.Repository<Pedido>()
            .GetAll().Where(u => u.IdComprador == idComprador && u.IdPedidosProductosNavigation.IdPreparacionNavigation.Estado == true )
            .Include(p => p.IdPedidosProductosNavigation)
            .ThenInclude(uu => uu.IdPreparacionNavigation.IdEnvioNavigation).ToListAsync();
          
              
        if (!ListapedidosPreparados.Any()) throw new Exception("No hay nada");
        var listapedidosDominio = ListapedidosPreparados.Select(p => GetListPreparationOrderMapper
            .ToDomain(p)).ToList();

        return listapedidosDominio;
    }

}