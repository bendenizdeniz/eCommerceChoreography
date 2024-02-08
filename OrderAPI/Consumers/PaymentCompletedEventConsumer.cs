using eCommerceChoreography.DataStructures;
using eCommerceChoreography.DataStructures.Enums;
using MassTransit;
using Shared.Events;

namespace OrderAPI.Consumers;

public class PaymentCompletedEventConsumer : IConsumer<PaymentCompletedEvent>
{
    private readonly OrderAPIDbContext _ctx;

    public PaymentCompletedEventConsumer(OrderAPIDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task Consume(ConsumeContext<PaymentCompletedEvent> context)
    {
        var msg = context.Message;

        var order = _ctx.Orders.FirstOrDefault(order => order.Id == msg.OrderId);

        if (order is null)
            throw new Exception("Order was not found.");

        order.OrderStatus = OrderStatus.Completed;
        _ctx.Update(order);
        await _ctx.SaveChangesAsync();
    }
}