using System.Linq;
using Xunit;
using shopApi.Handlers;
using shopApi.Commands;
using System.Threading.Tasks;
using shopApi.Models;
using Moq;
using System.Threading;

namespace tests.ProductHandler;

public class UpdateProductTest
{
    [Fact]
    public async Task UpdateProduct_Success()
    {
        var products = Helper.GetProducts();
        var updateProduct = new Product() { Id = 1, Name = "New Product", Price = 10, Description = "New Product description" };
        var mockdbSet = Helper.CreateMockProductDbSet(products);
        mockdbSet.Setup(x => x.FindAsync(updateProduct.Id)).ReturnsAsync(products.FirstOrDefault(x => x.Id == updateProduct.Id));

        var dataContext = new Mock<TestDataContext>(mockdbSet.Object);
        dataContext.Setup(x => x.Products).Returns(mockdbSet.Object);

        var handler = new UpdateProductHandler(dataContext.Object);
        var res = await handler.Handle(new UpdateProductCommand(updateProduct), default);

        dataContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        Assert.True(products.FirstOrDefault(x => x.Id == updateProduct.Id).Name == updateProduct.Name);
    }
}