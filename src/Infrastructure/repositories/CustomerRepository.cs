using Application.interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public CustomerRepository(AppDbContext context) : base(context) { }

    public async Task<Customer?> GetWithOrdersAsync(int id)
    {
        return await _context.Customers
            .Include(x => x.Orders)
            .ThenInclude(o => o.Items)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task<bool> EmailExistsAsync(string email, int? excludeCustomerId = null)
    {
        var query = _context.Customers.Where(x => x.Email == email);

        if (excludeCustomerId.HasValue)
            query = query.Where(x => x.Id != excludeCustomerId.Value);

        return await query.AnyAsync();
    }
    public async Task<List<TopCustomerDto>> GetTopCustomersAsync(int count)
    {
        return await _context.Customers
            .Include(x => x.Orders)
            .Select(c => new TopCustomerDto
            {
                CustomerId = c.Id,
                CustomerName = c.Name,
                Email = c.Email,
                TotalSpent = c.Orders.Sum(o => o.TotalPrice)
            })
            .OrderByDescending(x => x.TotalSpent)
            .Take(count)
            .ToListAsync();
    }
}