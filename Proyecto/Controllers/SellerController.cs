using System.Security.Claims;
using Application.IUseCase;
using Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto.Controllers;
[ApiController]
[Route("api/[controller]")]
public class SellerController (IOrderRequests orderRequests,IOrder order, ISendOrder sendOrder): ControllerBase
{
   
    
    [Authorize(Roles = "Vendedor")]
    [HttpGet("ObtenerSolicitudes/")]
   
    public async Task<IActionResult> GetSolicitud()
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized("No se encontró el ID de usuario en el token.");

            int userId = int.Parse(userIdClaim.Value);
            var registro = await orderRequests.GetSolicitud(userId);
            return Ok ( registro );
            
        }
        
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [Authorize(Roles = "Vendedor")]
    
    [HttpPut("AceptarSolicitud/{idPedido}")]
    public async Task<IActionResult> ActivarSolicitud(int idPedido)
    {
        try
        {
            var registro = await orderRequests.AceptarSolicitud(idPedido);
            return Ok ( registro );
            
        }
        
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [Authorize(Roles = "Vendedor")]
    
    [HttpGet("Confirmarsipago/{idPedido}")]
    public async Task<IActionResult> Versiapagado(int idPedido)
    {
        try
        {
            var registro = await orderRequests.VerSiPago(idPedido);
            return Ok (  new { Idproducto = registro });
            
        }
        
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [Authorize(Roles = "Vendedor")]
    
    [HttpGet("VerlistaPreparacion")]
    public async Task<IActionResult> listadepreparacion()
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized("No se encontró el ID de usuario en el token.");

            int userId = int.Parse(userIdClaim.Value);
            var registro = await order.ListGetPreparationOrder(userId);
            return Ok ( registro );
            
        }
        
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [HttpGet("InformacionPreviaPreparacion/{idproducto}")]
    public async Task<IActionResult> MostrarInformacionPreviaPreparacion(int idproducto)
    {
        try
        {
            
          
            var registro = await order.GetProveedorPreparationOrder(idproducto);
            return Ok ( registro );
            
        }
        
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    
    [Authorize(Roles = "Vendedor")]
    
    [HttpPost("PrepararProducto/{idProducto}")]
    public async Task<IActionResult> PrepararProducto(int idProducto, [FromBody] PreparationOrderDto preparationOrderDto )
    {
        try
        {
            var registro = await order.PreparetedOrder(idProducto, preparationOrderDto );
            return Ok (  new { IdPreparacion = registro } );
            
        }
        
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [Authorize(Roles = "Vendedor")]
    
    [HttpPost("EnviarProducto/{idPreparacion}")]
    public async Task<IActionResult> EnviarProducto(int idPreparacion, [FromBody] SendProductDto sendProductDto )
    {
        try
        {
            var registro = await sendOrder.EnviarProducto(idPreparacion, sendProductDto);
            return Ok (  new { IdEnvio = registro } );
            
        }
        
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    
    [Authorize(Roles = "Vendedor")]
    
    [HttpPost("ConfirmarEnvio/{idenvio}")]
    public async Task<IActionResult> ConfirmarEnvio(int idenvio )
    {
        try
        {
            var registro = await sendOrder.ConfirmarEnvio(idenvio);
            return Ok ( registro );
            
        }
        
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}