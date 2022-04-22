using MediatR;
using shopApi.Data;
using shopApi.Models;
using shopApi.Queries;

namespace shopApi.Handlers
{
    public class GetProductHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        private readonly IDataContext dataContext;

        public GetProductHandler(IDataContext context)
        {
            dataContext = context;
        }

        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await dataContext.Products.FindAsync(request.Id);
        }
    }
}