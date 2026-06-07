namespace Application.DTOs.Orders;

public class CreateOrderDto
{
    public int CustomerId { get; set; }

    public string Title { get; set; } = null!;

    public string Detail { get; set; } = null!;

    public List<CreateOrderItemDto> Items { get; set; }
        = new();
}