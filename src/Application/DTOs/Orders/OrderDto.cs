namespace Application.DTOs.Orders;

public class OrderDto
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string Title { get; set; } = null!;

    public decimal TotalPrice { get; set; }
}