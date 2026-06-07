using Application.DTOs.Customers;
using Application.interfaces;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Application.Services;

public class CustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(
        ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<PagedResult<CustomerDto>> GetAllAsync(int page, int pageSize)
    {
        if (page <= 0) page = 1;
        if (pageSize <= 0) pageSize = 10;

        var query = _customerRepository.Query();

        var totalCount = await query.CountAsync();

        var customers = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<CustomerDto>
        {
            Items = customers.Select(x => new CustomerDto
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email
            }).ToList(),
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }

    public async Task<CustomerDetailsDto?> GetByIdAsync(int id)
    {
        var customer =
            await _customerRepository
                .GetWithOrdersAsync(id);

        if (customer is null)
            return null;

        return new CustomerDetailsDto
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            Orders = customer.Orders
                .Select(o => new CustomerOrderDto
                {
                    Id = o.Id,
                    Title = o.Title,
                    TotalPrice = o.TotalPrice
                }).ToList()
        };
    }

    public async Task<int> CreateAsync(
        CreateCustomerDto dto)
    {
        if (await _customerRepository.EmailExistsAsync(dto.Email))
            throw new Exception("Email already exists");

        var customer = new Customer
        {
            Name = dto.Name,
            Email = dto.Email
        };

        await _customerRepository.AddAsync(customer);
        await _customerRepository.SaveChangesAsync();

        return customer.Id;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var customer = await _customerRepository
            .GetWithOrdersAsync(id);

        if (customer == null)
            return false;

        // 🚨 Business Rule
        if (customer.Orders.Any())
            throw new Exception("Customer has orders and cannot be deleted");

        _customerRepository.Delete(customer);
        await _customerRepository.SaveChangesAsync();

        return true;
    }
    public async Task<bool> UpdateAsync(int id, UpdateCustomerDto dto)
    {
        var customer = await _customerRepository.GetByIdAsync(id);

        if (customer == null)
            return false;

        customer.Name = dto.Name;
        customer.Email = dto.Email;

        _customerRepository.Update(customer);
        await _customerRepository.SaveChangesAsync();

        return true;
    }
}