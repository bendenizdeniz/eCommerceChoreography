using eCommerceChoreography.DataStructures.Enums;

namespace eCommerceChoreography.DataStructures;

public class Order : BaseEntity
{
    public Guid BuyerId { get; set; }

    public List<OrderItem> OrderList { get; set; }

    public OrderStatus OrderStatus { get; set; }

    public decimal TotalPrice { get; set; }
}