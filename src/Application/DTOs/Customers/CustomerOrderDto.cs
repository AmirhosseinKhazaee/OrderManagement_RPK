namespace Application.DTOs.Customers;

public class CustomerOrderDto
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public decimal TotalPrice { get; set; }
}