using MediatR;
using shopApi.Commands;
using shopApi.Data;
using shopApi.Models;

namespace shopApi.Handlers
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Product>
    {
        private readonly IDataContext dataContext;

        public UpdateProductHandler(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            Product product = await this.dataContext.Products.FindAsync(request.Product.Id);
            if (product == default)
                return null;

            UpdateProduct(product, request.Product);

            await this.dataContext.SaveChangesAsync(CancellationToken.None);

            return product;
        }

        private void UpdateProduct(Product product, Product newProductData)
        {
            product.Name = newProductData.Name;
            product.Description = newProductData.Description;
            product.Price = newProductData.Price;
        }
    }
}