namespace eCommerceChoreography.DataStructures;

public class OrderItem : BaseEntity
{
    public Guid ProductId { get; set; }

    public int Count { get; set; }

    public decimal Price { get; set; }
}