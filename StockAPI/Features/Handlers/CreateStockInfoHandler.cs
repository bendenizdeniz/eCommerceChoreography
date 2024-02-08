using MediatR;
using Microsoft.EntityFrameworkCore;
using StockAPI.DataStructures;
using StockAPI.Features.Requests;
using StockAPI.Features.Responses;

namespace StockAPI.Features.Handlers;

public class CreateStockInfoHandler : IRequestHandler<CreateStockInfoCommand, CreateStockInfoResponse>
{
    private readonly StockAPIDbContext _ctx;

    public CreateStockInfoHandler(StockAPIDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<CreateStockInfoResponse> Handle(CreateStockInfoCommand cmd, CancellationToken cancellationToken)
    {
        var stock = new Stock
        {
            ProductId = cmd.ProductId,
            Count = cmd.Count,
            CreatedDate = DateTime.UtcNow,
            IsDeleted = false
        };
        var res = await _ctx.Stocks.AddAsync(stock);
        _ctx.SaveChangesAsync();

        return new CreateStockInfoResponse
        {
            StockId = res.Entity.Id
        };
    }
}