using Xunit;
using shopApi.Queries;
using shopApi.Handlers;
using System.Threading.Tasks;
using System.Linq;

namespace tests.ProductHandler;

public class GetProductsTest
{
    [Fact]
    public async Task GetAllProducts()
    {
        var mockSet = Helper.CreateMockProductDbSet(Helper.GetProducts());
        var mockDataContext = Helper.CreateMockDataContext(mockSet);
        var handler = new GetProductsHandler(mockDataContext.Object);

        var result = await handler.Handle(new GetProductsQuery(), default);

        Assert.NotNull(result);
        Assert.Equal(result.Count(), Helper.GetProducts().Count());
    }
}