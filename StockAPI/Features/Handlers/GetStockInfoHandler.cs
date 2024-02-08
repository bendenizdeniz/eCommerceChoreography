using MediatR;
using Microsoft.EntityFrameworkCore;
using StockAPI.DataStructures;
using StockAPI.Features.Requests;
using StockAPI.Features.Responses;

namespace StockAPI.Features.Handlers;

public class GetStockInfoHandler : IRequestHandler<GetStockInfoQuery, GetStockInfoResponse>
{
    private readonly StockAPIDbContext _ctx;

    public GetStockInfoHandler(StockAPIDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<GetStockInfoResponse> Handle(GetStockInfoQuery query, CancellationToken cancellationToken)
    {
        var stockInfo = await _ctx.Stocks
            .FirstOrDefaultAsync(x => x.ProductId == query.ProductId);

        if (stockInfo is null)
            throw new Exception($"{query.ProductId} product is out of our stocks.");

        return new GetStockInfoResponse
        {
            ProductId = stockInfo.ProductId,
            Count = stockInfo.Count
        };
    }
}