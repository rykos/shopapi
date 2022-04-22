using MediatR;
using shopApi.Models;

namespace shopApi.Queries
{
    public record GetProductByIdQuery(long Id) : IRequest<Product>;
}