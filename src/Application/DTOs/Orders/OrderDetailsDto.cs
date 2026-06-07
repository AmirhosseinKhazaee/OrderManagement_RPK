namespace Application.DTOs.Orders;

public class OrderDetailsDto
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string Title { get; set; } = null!;

    public string Detail { get; set; } = null!;

    public decimal TotalPrice { get; set; }

    public List<OrderItemDto> Items { get; set; }
        = new();
}