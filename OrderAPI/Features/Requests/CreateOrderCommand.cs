using eCommerceChoreography.DataStructures;
using eCommerceChoreography.Features.Responses;
using MediatR;

namespace eCommerceChoreography.Features.Requests;

public class CreateOrderCommand : IRequest<CreateOrderResponse>
{
    public string CustomerId { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}