using System.Security.Claims;
using Application.CaseUse;
using Application.ICaseUse;
using Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
 
namespace Proyecto.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BuyerController (IOrder order, IPaymentOrder paymentOrder, ISendOrder sendOrder) : ControllerBase
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
            var registro = await order.RegisterOrder(registerOrderRequestDto , userId);
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
            var registro = await paymentOrder.VerificationPaymentPassword(userId, paymentPasswordDto.PaymentPassword);
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
            var registro = await order.VerSiOrderAceptado(idPedido);
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
            var registro = await paymentOrder.GeyDataPayment(idPedido);
            return Ok ( registro );
            
        }
        
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [Authorize(Roles = "Comprador")]
    [HttpPost("Pagar")]
    public async Task<IActionResult> Payment(int id, [FromBody] PaymentCartDto paymentCartDto)
    {
        try
        {
            var registro = await paymentOrder.Payment(id, paymentCartDto);
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

            var registro = await order.MostrarOrder(userId);
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

            var registro = await order.ListaMostrarPedidosPreparados(userId);
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

            var registro = await order.MostrarPedidosPreparados(userId , idPedido);
            return Ok (registro );
            
        }
        
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [Authorize(Roles = "Comprador")]
    [HttpGet("MostarEnvioDePedido/{idPreparacion}")]
    public async Task<IActionResult> MostrarEnvioPedido(int idPreparacion)
    
    {
        try
        {

            var registro = await sendOrder.verEnvio(idPreparacion);
            return Ok (registro );
            
        }
        
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    
    
}