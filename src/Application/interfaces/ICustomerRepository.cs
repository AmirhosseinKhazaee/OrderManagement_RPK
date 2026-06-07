
using Domain.Entities;

namespace Application.interfaces;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<Customer?> GetWithOrdersAsync(int id);
    Task<bool> EmailExistsAsync(string email);
    Task<List<TopCustomerDto>> GetTopCustomersAsync(int count);
}