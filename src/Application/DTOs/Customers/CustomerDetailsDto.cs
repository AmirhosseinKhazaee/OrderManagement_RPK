namespace Application.DTOs.Customers;

public class CustomerDetailsDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public List<CustomerOrderDto> Orders { get; set; }
        = new();
}