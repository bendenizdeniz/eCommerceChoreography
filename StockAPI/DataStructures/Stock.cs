namespace StockAPI.DataStructures;

public class Stock : BaseEntity
{
    public Guid ProductId { get; set; }
    public int Count { get; set; }

    public DateTime CreatedDate { get; set; }
}