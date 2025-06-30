using System.Security.Claims;
using Application.UseCase.Orders.Seller.Commands;
using Application.UseCase.Orders.Seller.Queries;
using Application.UseCase.SenddOrder.Seller.Commands;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto.Controllers;
[ApiController]
[Route("api/[controller]")]
public class SellerController (IMediator _mediator ): ControllerBase
{
   
    
    [Authorize(Roles = "Vendedor")]
    [HttpGet("ObtenerSolicitudes/")]
   
    public async Task<IActionResult> GetSolicitud(CancellationToken cancellationToken)
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized("No se encontró el ID de usuario en el token.");

            int userId = int.Parse(userIdClaim.Value);
            var registro = await _mediator.Send(new GetPendingOrdersQuery(userId), cancellationToken );;
            return Ok ( registro );
            
        }
        
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [Authorize(Roles = "Vendedor")]
    
    [HttpPut("AceptarSolicitud/{idPedido}")]
    public async Task<IActionResult> ActivarSolicitud(int idPedido, CancellationToken cancellationToken)
    {
        try
        {
            var registro = await _mediator.Send(new AcceptOrderRequestCommand(idPedido), cancellationToken );
            return Ok ( registro );
            
        }
        
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [Authorize(Roles = "Vendedor")]
    
    [HttpGet("Confirmarsipago/{idPedido}")]
    public async Task<IActionResult> Versiapagado(int idPedido, CancellationToken cancellationToken)
    {
        try
        {
            
            var registro = await _mediator.Send(new CheckIfOrderIsPaidQuery(idPedido), cancellationToken );
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
            

            var registro = await _mediator.Send(new GetSellerPreparedOrdersListQuery(userId) );

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
            
            var registro = await _mediator.Send(new GetPreparationPreviewQuery(idproducto) );

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
           
            var registro = await _mediator.Send(new PrepareOrderCommand(idProducto, preparationOrderDto) );
            return Ok (  new { IdPreparacion = registro } );
            
        }
        
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [Authorize(Roles = "Vendedor")]
    
    [HttpPost("EnviarProducto/{idPreparacion}")]
    public async Task<IActionResult> EnviarProducto(int idPreparacion, [FromBody] SendProductDto sendProductDto, CancellationToken cancellationToken )
    {
        try
        {
           
            var registro =  await _mediator.Send(new SendProductCommand(idPreparacion, sendProductDto) , cancellationToken );

            return Ok (  new { IdEnvio = registro } );
            
        }
        
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    
    [Authorize(Roles = "Vendedor")]
    
    [HttpPost("ConfirmarEnvio/{idenvio}")]
    public async Task<IActionResult> ConfirmarEnvio(int idenvio, CancellationToken cancellationToken )
    {
        try
        {
             
            var registro = await _mediator.Send(new ConfirmShipmentCommand(idenvio) , cancellationToken );
            ;
            return Ok ( registro );
            
        }
        
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}