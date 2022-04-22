using MediatR;
using shopApi.Models;

namespace shopApi.Queries
{
    public record GetProductsQuery() : IRequest<List<Product>>;
}