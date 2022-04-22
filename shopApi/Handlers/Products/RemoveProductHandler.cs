using MediatR;
using shopApi.Commands;
using shopApi.Data;
using shopApi.Models;

namespace shopApi.Handlers
{
    public class RemoveProductHandler : IRequestHandler<RemoveProductByIdCommand, bool>
    {
        private readonly IDataContext dataContext;

        public RemoveProductHandler(IDataContext context)
        {
            dataContext = context;
        }

        public async Task<bool> Handle(RemoveProductByIdCommand request, CancellationToken cancellationToken)
        {
            Product product = await dataContext.Products.FindAsync(request.Id);
            if (product == null)
                return false;

            dataContext.Products.Remove(product);

            await dataContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}