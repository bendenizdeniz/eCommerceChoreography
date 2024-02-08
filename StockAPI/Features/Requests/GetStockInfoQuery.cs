using MediatR;
using StockAPI.Features.Responses;

namespace StockAPI.Features.Requests;

public class GetStockInfoQuery : IRequest<GetStockInfoResponse>
{
    public Guid ProductId { get; set; }
}