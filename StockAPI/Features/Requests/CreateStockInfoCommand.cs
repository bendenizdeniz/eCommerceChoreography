using MediatR;
using StockAPI.Features.Responses;

namespace StockAPI.Features.Requests;

public class CreateStockInfoCommand : IRequest<CreateStockInfoResponse>
{
    public Guid ProductId { get; set; }

    public int Count { get; set; }
}