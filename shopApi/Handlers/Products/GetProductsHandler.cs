using MediatR;
using Microsoft.EntityFrameworkCore;
using shopApi.Data;
using shopApi.Models;
using shopApi.Queries;

namespace shopApi.Handlers
{
    public class GetProductsHandler : IRequestHandler<GetProductsQuery, List<Product>>
    {
        private readonly IDataContext dataContext;

        public GetProductsHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public Task<List<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return this.dataContext.Products.ToListAsync();
        }
    }
}