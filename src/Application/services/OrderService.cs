using Application.DTOs.Orders;
using Application.interfaces;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Application.Services;
public class OrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;

    public OrderService(
        IOrderRepository orderRepository,
        ICustomerRepository customerRepository)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
    }

    public async Task<int> CreateAsync(CreateOrderDto dto)
    {
        var customer = await _customerRepository.GetByIdAsync(dto.CustomerId);

        if (customer == null)
            throw new Exception("Customer not found");

        var order = new Order
        {
            CustomerId = dto.CustomerId,
            Title = dto.Title,
            Detail = dto.Detail,
            Items = new List<OrderItem>()
        };

        decimal total = 0;

        foreach (var item in dto.Items)
        {
            var orderItem = new OrderItem
            {
                ProductName = item.ProductName,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice
            };

            order.Items.Add(orderItem);

            total += item.Quantity * item.UnitPrice;
        }

        order.TotalPrice = total;

        await _orderRepository.AddAsync(order);
        await _orderRepository.SaveChangesAsync();

        return order.Id;
    }
    public async Task<PagedResult<OrderDto>> GetAllAsync(int page, int pageSize)
    {
        if (page <= 0) page = 1;
        if (pageSize <= 0) pageSize = 10;

        var query = _orderRepository.Query(); 

        var totalCount = await query.CountAsync();

        var orders = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var result = orders.Select(x => new OrderDto
        {
            Id = x.Id,
            CustomerId = x.CustomerId,
            Title = x.Title,
            TotalPrice = x.TotalPrice
        }).ToList();

        return new PagedResult<OrderDto>
        {
            Items = result,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }
    public async Task<OrderDetailsDto?> GetByIdAsync(int id)
    {
        var order = await _orderRepository.GetOrderWithItemsAsync(id);

        if (order == null)
            return null;

        return new OrderDetailsDto
        {
            Id = order.Id,
            CustomerId = order.CustomerId,
            Title = order.Title,
            Detail = order.Detail,
            TotalPrice = order.TotalPrice,
            Items = order.Items.Select(i => new OrderItemDto
            {
                Id = i.Id,
                ProductName = i.ProductName,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList()
        };
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);

        if (order == null)
            return false;

        _orderRepository.Delete(order);
        await _orderRepository.SaveChangesAsync();

        return true;
    }
    public async Task<bool> UpdateAsync(int id, UpdateOrderDto dto)
    {
        var order = await _orderRepository.GetByIdAsync(id);

        if (order == null)
            return false;

        order.Title = dto.Title;
        order.Detail = dto.Detail;

        _orderRepository.Update(order);
        await _orderRepository.SaveChangesAsync();

        return true;
    }
}