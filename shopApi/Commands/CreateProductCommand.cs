using MediatR;
using shopApi.Models;

namespace shopApi.Commands
{
    public record CreateProductCommand(
        string Name,
        string Description,
        double Price) : IRequest<Product>;
}