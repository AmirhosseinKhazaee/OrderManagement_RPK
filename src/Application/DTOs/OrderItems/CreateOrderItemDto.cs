namespace Application.DTOs.Orders;

public class CreateOrderItemDto
{
    public string ProductName { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }
}