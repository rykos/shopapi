using MediatR;
using shopApi.Models;

namespace shopApi.Commands
{
    public record UpdateProductCommand(Product Product) : IRequest<Product>;
}