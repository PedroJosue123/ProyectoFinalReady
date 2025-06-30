using System.Security.Claims;
using Application.IUseCase;
using Application.UseCase.Orders.Buyer.Commands;
using Application.UseCase.Orders.Buyer.Queries;
using Application.UseCase.PaymenttOrder.Buyer.Commands;
using Application.UseCase.PaymenttOrder.Buyer.Queries;
using Application.UseCase.SenddOrder.Buyer.Queries;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
 
namespace Proyecto.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BuyerController (IMediator _mediator, IOrder order, IPaymentOrder paymentOrder, ISendOrder sendOrder) : ControllerBase
{
   
    [Authorize(Roles = "Comprador")]
    [HttpPost("RegistrarPedido")]
    public async Task<IActionResult> Login([FromBody] RegisterOrderRequestDto registerOrderRequestDto)
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized("No se encontró el ID de usuario en el token.");
            int userId = int.Parse(userIdClaim.Value);
            var registro = await _mediator.Send(new RegisterOrderCommand(registerOrderRequestDto , userId ));;
            return Ok (new { Idpedido = registro });
            
        }
        
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [Authorize(Roles = "Comprador")]
    [HttpPost("ComprobarContraseñaparaPago")]
    public async Task<IActionResult> ComprobarContraseña([FromBody] PaymentPasswordDto paymentPasswordDto)
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized("No se encontró el ID de usuario en el token.");
            int userId = int.Parse(userIdClaim.Value);
            var registro = await _mediator.Send(new VerifyPaymentPasswordCommand(userId, paymentPasswordDto.PaymentPassword));

            return Ok (registro);
            
        }
        
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [Authorize(Roles = "Comprador")]
    [HttpGet("VerSifueAceptado/{idPedido}")]
    public async Task<IActionResult> VersiPago(int idPedido)
    {
        try
        {
            var registro = await _mediator.Send(new CheckOrderAcceptedQuery (idPedido));
            return Ok (new { Idpedido = registro });
            
        }
        
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [Authorize(Roles = "Comprador")]
    [HttpGet("VerVistaPago/{idPedido}")]
    public async Task<IActionResult> GetPayment(int idPedido)
    {
        try
        {
          
            var registro =  await _mediator.Send(new GetPaymentDataQuery (idPedido));
            return Ok ( registro );
            
        }
        
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [Authorize(Roles = "Comprador")]
    [HttpPost("Pagar")]
    public async Task<IActionResult> Payment(int OrderId, [FromBody] PaymentCartDto paymentCartDto)
    {
        try
        {
           
            var registro = await _mediator.Send(new ProcessPaymentCommand(OrderId, paymentCartDto));
            return Ok (registro);
            
        }
        
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [Authorize(Roles = "Comprador")]
    [HttpGet("Mostarlospedidos")]
    public async Task<IActionResult> MostrarOrder()
    
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized("No se encontró el ID de usuario en el token.");

            int userId = int.Parse(userIdClaim.Value);

            var registro =  await _mediator.Send(new GetBuyerOrdersQuery(userId));
            return Ok (registro);
            
        }
        
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    [Authorize(Roles = "Comprador")]
    [HttpGet("ListapedidosPreparados")]
    public async Task<IActionResult> ListapedidosPreparados()
    
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized("No se encontró el ID de usuario en el token.");

            int userId = int.Parse(userIdClaim.Value);

            var registro = await _mediator.Send(new GetPreparedOrdersListQuery(userId));
            return Ok (registro );
            
        }
        
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    [Authorize(Roles = "Comprador")]
    [HttpGet("MostarlospedidosPreparados/{idPedido}")]
    public async Task<IActionResult> MostrarPreparados(int idPedido)
    
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized("No se encontró el ID de usuario en el token.");

            int userId = int.Parse(userIdClaim.Value);

            var registro = await _mediator.Send(new GetPreparedOrderDetailQuery(userId, idPedido));
            return Ok (registro );
            
        }
        
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [Authorize(Roles = "Comprador")]
    [HttpGet("MostarEnvioDePedido/{idPreparacion}")]
    public async Task<IActionResult> MostrarEnvioPedido(int idPreparacion , CancellationToken cancellationToken)
    
    {
        try
        {
           

            var registro = await _mediator.Send(new GetShipmentStatusQuery(idPreparacion) , cancellationToken);
            return Ok (registro );
            
        }
        
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    
    
}