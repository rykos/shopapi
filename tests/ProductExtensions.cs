using shopApi.Models;

namespace tests;

public static class ProductExtensions
{
    public static object StripDateTime(this Product product)
    {
        return new
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price
        };
    }
}