using Domain.Dtos;
using Domain.Entities;

namespace Application.IUseCase;

public interface IOrder
{
    Task<int> RegisterOrder(RegisterOrderRequestDto requestDto, int id);
    Task<List<GetListPrepationDomain>> ListGetPreparationOrder(int id);
    Task<GetSellerOrderDomain> GetProveedorPreparationOrder(int idPedidosProducto);
    Task<int> PreparetedOrder(int id, PreparationOrderDto preparationOrderDto);
    Task<bool> VerSiOrderAceptado(int id);
    Task<List<GetOrderDomain>> MostrarOrder(int id);
    Task<GetPreparationOrderDomain> MostrarPedidosPreparados(int idPedido, int idComprador);
    Task<List<GetListPreparationOrderDomain>> ListaMostrarPedidosPreparados(int idComprador);
}