using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockAPI.Features.Requests;
using StockAPI.Features.Responses;

namespace StockAPI.Controllers;

[ApiController, Route("orders")]
public class StockController : ControllerBase
{
    private IMediator _mediator;

    public StockController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create-stock-info")]
    public async Task<CreateStockInfoResponse> CreateStockInfo(CreateStockInfoCommand cmd)
    {
        return await _mediator.Send(cmd);
    }

    [HttpPost("get-stock-info")]
    public async Task<GetStockInfoResponse> GetStockInfo(GetStockInfoQuery cmd)
    {
        return await _mediator.Send(cmd);
    }
}