using eCommerceChoreography.Features.Responses;
using MediatR;

namespace eCommerceChoreography.Features.Requests;

public class CreateOrderItemCommand : IRequest<CreateOrderItemResponse>
{
    public string ProductId { get; set; }

    public int Count { get; set; }

    public decimal Price { get; set; }
}