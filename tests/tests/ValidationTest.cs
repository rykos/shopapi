using Xunit;
using shopApi.Models;
using System.ComponentModel.DataAnnotations;

namespace tests.ProductHandler;

public class ValidationTest
{
    [Theory]
    [InlineDataAttribute(0, "Name", 0, "Product 2 description")]
    [InlineDataAttribute(long.MaxValue, "Name", double.MaxValue, "Product 2 description")]
    [InlineDataAttribute(50, "Name", 50, "Product 2 description")]
    public void ProductValidation_Success(long id, string name, double price, string description)
    {
        Product product = new Product() { Id = id, Name = name, Price = price, Description = description };
        var validationContext = new ValidationContext(product);
        bool isValid = Validator.TryValidateObject(product, validationContext, null, true);
        Assert.True(isValid);
    }

    [Theory]
    [InlineDataAttribute(0, "Negative price", -1, "Product 2 description")]//Negative price
    [InlineDataAttribute(0, null, 0, "Product 2 description")]//No name
    [InlineDataAttribute(0, "Name", 0, null)]//No description
    public void ProductValidation_Fail(long id, string name, double price, string description)
    {
        Product product = new Product() { Id = id, Name = name, Price = price, Description = description };
        var validationContext = new ValidationContext(product);
        bool isValid = Validator.TryValidateObject(product, validationContext, null, true);
        Assert.False(isValid);
    }
}