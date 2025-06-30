namespace Application.UseCase;

public class PaymentOrder(IUnitOfWork unitOfWork, IPaymentServer paymentServer, IAuthService _authService) : IPaymentOrder
{
    public async Task<bool> VerificationPaymentPassword(int iduser, string password)
    {
        var user = await unitOfWork.Repository<Userprofile>().GetAll()
            .Where(u => u.UserId == iduser)
            .FirstOrDefaultAsync();
        
        if (!_authService.VerifyPassword(password, user.PaymentPassword)) throw new Exception("Contraseña Incorrecta");
        
        return true;


    }
    
    
    public async Task<PaymentGetRequestDomain> GeyDataPayment(int id)
    {
        var Pedido = await unitOfWork.Repository<Pedido>().GetAll()
            .Include(p => p.IdPedidosProductosNavigation)
            .ThenInclude(pp => pp.IdPagoNavigation)
            .FirstOrDefaultAsync(u => u.IdPedido == id && u.Estado == true);
        if (Pedido == null) throw new Exception("No hay ni mierda");
        var pedidodomain = PaymentGetOrderMapper.ToDomain(Pedido);

      
        
        return pedidodomain;


    }
    
    public async Task<Boolean> Payment(int id, PaymentCartDto card)
    {
        var Pedido = await unitOfWork.Repository<Pedido>().GetAll()
            .Include(p => p.IdPedidosProductosNavigation)
            .ThenInclude(pp => pp.IdPagoNavigation)
            .FirstOrDefaultAsync(u => u.IdPedido == id && u.Estado == true);
        
        
        if (Pedido == null) throw new Exception("No hay ni mierda");

        var validatecard = paymentServer.paymentt(card.NroTarjeta, card.FechaNacimiento, card.cvv);

        if (!validatecard) throw new Exception("No se puede pagar");
        
        Pedido.IdPedidosProductosNavigation.IdPagoNavigation.Estado = true;
        Pedido.IdPedidosProductosNavigation.IdPagoNavigation.FechaPago =  DateTime.Now;
        await unitOfWork.SaveChange();
        return true;

    }
    
}