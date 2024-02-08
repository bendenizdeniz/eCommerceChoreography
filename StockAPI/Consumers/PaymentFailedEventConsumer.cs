using MassTransit;
using Shared.Events;
using StockAPI.DataStructures;

namespace StockAPI.Consumers;

public class PaymentFailedEventConsumer : IConsumer<PaymentFailedEvent>
{
    private readonly StockAPIDbContext _ctx;

    public PaymentFailedEventConsumer(StockAPIDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task Consume(ConsumeContext<PaymentFailedEvent> context)
    {
        var msg = context.Message;

        foreach (var orderItem in msg.OrderItems)
        {
            var stockInfo = _ctx.Stocks.FirstOrDefault(stock => stock.ProductId == orderItem.ProductId);

            if (stockInfo is null) throw new Exception("An error occured while accessing to product's stock info");

            stockInfo.Count += orderItem.Count;
            _ctx.Update(stockInfo);
            await _ctx.SaveChangesAsync();
        }

        throw new NotImplementedException();
    }
}