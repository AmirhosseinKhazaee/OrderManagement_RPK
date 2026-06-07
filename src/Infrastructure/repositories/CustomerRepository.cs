using Application.interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

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
    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _context.Customers
            .AnyAsync(x => x.Email == email);
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