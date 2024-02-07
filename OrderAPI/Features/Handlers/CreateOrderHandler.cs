using eCommerceChoreography.DataStructures;
using eCommerceChoreography.DataStructures.Enums;
using eCommerceChoreography.Features.Requests;
using eCommerceChoreography.Features.Responses;
using MassTransit;
using MediatR;
using Shared.Events;
using Shared.Extensions;
using Shared.Messages;

namespace eCommerceChoreography.Features.Handlers;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, CreateOrderResponse>
{
    private readonly OrderAPIDbContext _ctx;
    private readonly IPublishEndpoint _publishEndpoint;

    public CreateOrderHandler(OrderAPIDbContext ctx, IPublishEndpoint publishEndpoint)
    {
        _ctx = ctx;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<CreateOrderResponse> Handle(CreateOrderCommand cmd, CancellationToken cancellationToken)
    {
        var buyerId = OrderExtensions.StringToGuid("CustomerId", cmd.CustomerId);

        var orderItems = cmd.OrderItems.Select(x => new OrderItem
        {
            Count = x.Count,
            Price = x.Price,
            ProductId = x.ProductId,
            IsDeleted = false,
            CreatedDate = DateTime.Now,
        }).ToList();

        var order = new Order
        {
            BuyerId = buyerId,
            OrderList = orderItems,
            OrderStatus = OrderStatus.Suspend,
            CreatedDate = DateTime.Now,
            TotalPrice = cmd.OrderItems.Sum(x => x.Price * x.Count)
        };

        var res = await _ctx.Orders.AddAsync(order);
        _ctx.SaveChangesAsync();


        var orderCreatedEvent = new OrderCreatedEvent
        {
            BuyerId = buyerId,
            OrderId = order.Id,
            TotalPrice = order.TotalPrice,
            OrderItems = order.OrderList.Select(x => new OrderItemMessage
            {
                Count = x.Count,
                Price = x.Price,
                ProductId = x.ProductId
            }).ToList()
        };
        _publishEndpoint.Publish(orderCreatedEvent);

        return new CreateOrderResponse()
        {
            OrderId = res.Entity.Id
        };
    }
}