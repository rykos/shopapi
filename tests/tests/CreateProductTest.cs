using Xunit;
using shopApi.Handlers;
using System.Threading.Tasks;
using shopApi.Commands;
using System.Linq;
using Moq;
using shopApi.Models;
using System.Threading;
using System.Collections.Generic;

namespace tests.ProductHandler;

public class CreateProductTest
{
    [Fact]
    public async Task CreateProduct_Success()
    {
        var productsToCreate = Helper.GetProducts();
        var products = new List<Product>();
        var mockdbSet = Helper.CreateMockProductDbSet(products);
        mockdbSet.Setup(set => set.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
            .Callback((Product product, CancellationToken _) => products.Add(product));

        var dataContext = Helper.CreateMockDataContext(mockdbSet);
        var handler = new CreateProductHandler(dataContext.Object);

        foreach (Product product in productsToCreate)
        {
            var result = await handler.Handle(new CreateProductCommand(product.Name, product.Description, product.Price), default);
            Assert.NotNull(result);
        }

        dataContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(productsToCreate.Count()));
        Assert.True(dataContext.Object.Products.Count() == productsToCreate.Count());
    }
}