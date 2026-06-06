
namespace Domain.Entities;
public class Order : BaseEntity
{
    public int CustomerId { get; set; }

    public Customer Customer { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Detail { get; set; } = null!;

    public decimal TotalPrice { get; set; }

    public ICollection<OrderItem> Items { get; set; }
        = new List<OrderItem>();
}