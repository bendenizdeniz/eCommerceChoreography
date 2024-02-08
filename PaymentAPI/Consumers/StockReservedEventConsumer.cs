using MassTransit;
using Shared.Events;

namespace PaymentAPI.Consumers;

public class StockReservedEventConsumer : IConsumer<StockReservedEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;

    public StockReservedEventConsumer(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<StockReservedEvent> context)
    {
        //!!Payment system operations skipped in this project. I aim to communication between services via events.

        var msg = context.Message;

        if (true) //payment operations ok.
        {
            var paymentSuccessEvent = new PaymentCompletedEvent
            {
                BuyerId = msg.BuyerId,
                OrderId = msg.OrderId,
                OrderItems = msg.OrderItems
            };

            await _publishEndpoint.Publish(paymentSuccessEvent);
        }
        else //payment operations has failed.
        {
            var paymentFailedEvent = new PaymentFailedEvent
            {
                BuyerId = msg.BuyerId,
                OrderId = msg.OrderId,
                OrderItems = msg.OrderItems,
                Message =
                    "Payment operation has failed." //it could be various reason like unsufficient balance, payment service error, etc..
            };

            await _publishEndpoint.Publish(paymentFailedEvent);
        }
    }
}