using System.Collections.Generic;
using System.Linq;
using MockQueryable.Moq;
using Moq;
using shopApi.Models;
using Microsoft.EntityFrameworkCore;

namespace tests
{
    public class Helper
    {
        public static List<Product> GetProducts() => new List<Product>
        {
            new Product { Id = 0, Name = "Product 1", Price = 10, Description = "Product 1 description" },
            new Product { Id = 1, Name = "Product 2", Price = 20, Description = "Product 2 description" },
            new Product { Id = 2, Name = "Product 3", Price = 30, Description = "Product 3 description" }
        };

        public static TestDataContext CreateIDataContext(List<Product> products = null)
        {
            var mock = CreateMockProductDbSet(products);
            mock.Setup(x => x.Add(It.IsAny<Product>())).Verifiable();
            mock.Setup(x => x.Remove(It.IsAny<Product>())).Verifiable();

            return new TestDataContext(mock.Object);
        }

        public static Mock<DbSet<Product>> CreateMockProductDbSet(IEnumerable<Product> products)
        {
            return products.AsQueryable().BuildMockDbSet();
        }

        public static Mock<TestDataContext> CreateMockDataContext(Mock<DbSet<Product>> products)
        {
            var mockDataContext = new Mock<TestDataContext>(products.Object);
            mockDataContext.Setup(x => x.Products).Returns(products.Object);
            mockDataContext.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1).Verifiable();
            
            return mockDataContext;
        }
    }
}