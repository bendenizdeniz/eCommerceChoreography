using MassTransit;
using Shared;
using Shared.Events;
using StockAPI.DataStructures;

namespace StockAPI.Consumers;

public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
{
    private readonly StockAPIDbContext _ctx;
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public OrderCreatedEventConsumer(StockAPIDbContext ctx, ISendEndpointProvider sendEndpointProvider)
    {
        _ctx = ctx;
        _sendEndpointProvider = sendEndpointProvider;
    }

    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var stocksAvailability = new List<bool>();
        var msg = context.Message;
        var stocks = _ctx.Stocks.ToList();

        foreach (var orderItem in context.Message.OrderItems)
        {
            var _currentStock = stocks.FirstOrDefault(stock =>
                stock.ProductId == orderItem.ProductId && stock.Count >= orderItem.Count);

            if (_currentStock is null)
                throw new Exception("Stock is undefined.");

            stocksAvailability.Add(_currentStock is not null);
        }

        if (stocksAvailability.TrueForAll(x => x.Equals(true)))
        {
            foreach (var orderItem in msg.OrderItems)
            {
                var _currentStock = stocks.FirstOrDefault(stock => stock.ProductId == orderItem.ProductId);

                if (_currentStock is null)
                    throw new Exception("Stock is undefined.");

                _currentStock.Count -= orderItem.Count;
                _ctx.Update(_currentStock);
                _ctx.SaveChangesAsync();
            }

            //stock guncellemesi yapildi,odeme icin event firlatilabilir.
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(
                new Uri($"queue:{RabbitMqConfig.Payment_StockReservedEventQueue}"));

            var stockReservedEvent = new StockReservedEvent
            {
                BuyerId = msg.BuyerId,
                OrderId = msg.OrderId,
                TotalPrice = msg.TotalPrice,
                OrderItems = msg.OrderItems
            };

            await sendEndpoint.Send(stockReservedEvent);
        }
        else
        {
            //Stock yetersiz ürünler mevcut. Hata event
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(
                new Uri($"queue:{RabbitMqConfig.Payment_StockNotReservedEventQueue}"));

            var stockReservedEvent = new StockNotReservedEvent
            {
                BuyerId = msg.BuyerId,
                OrderId = msg.OrderId,
                Message = "Stock info is insufficient for this product."
            };

            await sendEndpoint.Send(stockReservedEvent);
        }
    }
}