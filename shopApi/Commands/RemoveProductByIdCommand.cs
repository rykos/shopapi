using MediatR;

namespace shopApi.Commands
{
    public record RemoveProductByIdCommand(long Id) : IRequest<bool>;
}