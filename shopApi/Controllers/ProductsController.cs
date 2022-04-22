using MediatR;
using Microsoft.AspNetCore.Mvc;
using shopApi.Commands;
using shopApi.Models;
using shopApi.Queries;

namespace shopApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public readonly IMediator mediator;

        public ProductsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<List<Product>> GetProducts()
        {
            return await mediator.Send(new GetProductsQuery());
        }

        [HttpGet("{id}")]
        public async Task<Product> GetProductById(long id)
        {
            return await mediator.Send(new GetProductByIdQuery(id));
        }

        [HttpPost]
        public async Task<Product> CreateProduct([FromBody] Product product)
        {
            return await mediator.Send(new CreateProductCommand(product.Name, product.Description, product.Price));
        }

        [HttpPut]
        public async Task<Product> UpdateProduct([FromBody] Product product)
        {
            return await mediator.Send(new UpdateProductCommand(product));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            return await mediator.Send(new RemoveProductByIdCommand(id)) ? Ok() : (IActionResult)NotFound();
        }
    }
}