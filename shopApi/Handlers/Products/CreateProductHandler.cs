using MediatR;
using shopApi.Commands;
using shopApi.Data;
using shopApi.Models;

namespace shopApi.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly IDataContext dataContext;

        public CreateProductHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price
            };

            await dataContext.Products.AddAsync(product);
            await dataContext.SaveChangesAsync(CancellationToken.None);

            return product;
        }
    }
}