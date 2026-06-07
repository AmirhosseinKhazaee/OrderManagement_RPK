using Application.interfaces;
using Domain.Entities;

namespace Application.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    Task<Order?> GetOrderWithItemsAsync(int id);

    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
    
}