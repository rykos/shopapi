using Xunit;
using shopApi.Queries;
using shopApi.Handlers;
using System.Threading.Tasks;
using Moq;
using System.Linq;

namespace tests.ProductHandler;

public class GetProductByIdTest
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public async Task GetProductById_Success(long value)
    {
        var products = Helper.GetProducts();
        var dataContext = GetDataContext(value);
        var handler = new GetProductHandler(dataContext);

        var result = await handler.Handle(new GetProductByIdQuery(value), default);

        Assert.NotNull(result);
        Assert.Equal(result.Name, products[(int)value].Name);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(4)]
    public async Task GetProductById_Fail(long value)
    {
        var dataContext = GetDataContext(value);
        var handler = new GetProductHandler(dataContext);

        var result = await handler.Handle(new GetProductByIdQuery(value), default);

        Assert.Null(result);
    }

    private TestDataContext GetDataContext(long value)
    {
        var products = Helper.GetProducts();
        var mockset = Helper.CreateMockProductDbSet(products);
        mockset.Setup(x => x.FindAsync(It.IsAny<long>()))
            .ReturnsAsync((object[] ids) => products.FirstOrDefault(x => x.Id == value));
        var dataContextMock = Helper.CreateMockDataContext(mockset);
        return dataContextMock.Object;
    }
}