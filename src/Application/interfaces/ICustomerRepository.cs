
using Domain.Entities;

namespace Application.interfaces;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<Customer?> GetWithOrdersAsync(int id);
    Task<bool> EmailExistsAsync(string email, int? excludeCustomerId = null);
    Task<List<TopCustomerDto>> GetTopCustomersAsync(int count);
}