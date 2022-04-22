using Moq;
using shopApi.Handlers;
using shopApi.Commands;
using shopApi.Models;
using System.Linq;
using System.Threading;
using Xunit;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace tests.ProductHandler;

public class DeleteProductTest
{
    private List<Product> products;
    public DeleteProductTest()
    {
        products = Helper.GetProducts();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void DeleteProduct_Success(long id)
    {
        var mockdbSet = Helper.CreateMockProductDbSet(products);
        var dataContext = new Mock<TestDataContext>(mockdbSet.Object);
        ConfigureMockDbSet(mockdbSet, dataContext, id);

        var handler = new RemoveProductHandler(dataContext.Object);

        var res = handler.Handle(new RemoveProductByIdCommand(id), default);

        dataContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        mockdbSet.Verify(x => x.Remove(It.IsAny<Product>()), Times.Once);
        Assert.DoesNotContain(dataContext.Object.Products, x => x.Id == id);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(3)]
    [InlineData(long.MaxValue)]
    public void DeleteProduct_Fail(long id)
    {
        var mockdbSet = Helper.CreateMockProductDbSet(products);
        var dataContext = new Mock<TestDataContext>(mockdbSet.Object);
        ConfigureMockDbSet(mockdbSet, dataContext, id);

        var handler = new RemoveProductHandler(dataContext.Object);

        var res = handler.Handle(new RemoveProductByIdCommand(id), default);

        dataContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        mockdbSet.Verify(x => x.Remove(It.IsAny<Product>()), Times.Never);
    }

    private void ConfigureMockDbSet(Mock<DbSet<Product>> mockdbSet, Mock<TestDataContext> dataContext, long id)
    {
        mockdbSet.Setup(x => x.FindAsync(It.IsAny<long>()))
            .ReturnsAsync(products.FirstOrDefault(x => x.Id == id));
        mockdbSet.Setup(x => x.Remove(It.IsAny<Product>()))
            .Callback((Product x) => { products.Remove(x); });

        dataContext.Setup(x => x.Products).Returns(mockdbSet.Object);
    }
}