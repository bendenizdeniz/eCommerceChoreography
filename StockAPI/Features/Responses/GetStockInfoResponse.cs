namespace StockAPI.Features.Responses;

public class GetStockInfoResponse
{
    public Guid ProductId { get; set; }

    public int Count { get; set; }
}