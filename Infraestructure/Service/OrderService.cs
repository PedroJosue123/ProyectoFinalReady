/*
using Domain.Dtos;
using Domain.Interface;
using Infraestructure.Models;

namespace Infraestructure.Service;

public class OrderService
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> CreateOrderAsync(CreateOrderDto dto, int userId)
    {
        var order = new order
        {
            Product = dto.Product,
            Quantity = dto.Quantity,
            Supplier = dto.Supplier,
            Status = dto.Status,
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Repository<order>().AddAsync(order);
        await _unitOfWork.SaveChange();
        return true;
    }

    public async Task<IEnumerable<order>> GetOrdersByUserAsync(int userId)
    {
        var orders = await _unitOfWork.Repository<order>().GetALLAsync();
        return orders.Where(o => o.UserId == userId);
    }
}*/