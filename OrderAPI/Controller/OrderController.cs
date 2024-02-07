using eCommerceChoreography.DataStructures;
using eCommerceChoreography.DataStructures.Enums;
using eCommerceChoreography.Features.Requests;
using eCommerceChoreography.Features.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceChoreography.Controller;

[ApiController, Route("orders")]
public class OrderController : ControllerBase
{
    private IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create-order")]
    public async Task<CreateOrderResponse> CreateOrder(CreateOrderCommand cmd)
    {
        return await _mediator.Send(cmd);
    }
}