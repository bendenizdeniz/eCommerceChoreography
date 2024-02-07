using eCommerceChoreography.Features.Requests;
using eCommerceChoreography.Features.Responses;
using MediatR;

namespace eCommerceChoreography.Features.Handlers;

public class CreateOrderItemHandler : IRequestHandler<CreateOrderItemCommand, CreateOrderItemResponse>
{
    public Task<CreateOrderItemResponse> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}