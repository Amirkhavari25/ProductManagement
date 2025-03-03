using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Features.Products.Commands.CreateProduct;
using ProductManagement.Application.Features.Products.Commands.DeleteProduct;
using ProductManagement.Application.Features.Products.Commands.UpdateProduct;
using ProductManagement.Application.Features.Products.Queries.GetProductById;
using ProductManagement.Application.Features.Products.Queries.GetProducts;

namespace ProductManagement.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            var productId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = productId }, productId);
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> GetAll()
        {
            var products = await _mediator.Send(new GetProductQuery());
            return Ok(products);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(Id));
            return Ok(product);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(Guid Id, [FromBody] UpdateProductCommand command)
        {
            if (Id != command.Id)
            {
                return BadRequest("Product ID mismatch.");
            }
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            await _mediator.Send(new DeleteProductCommand(Id));
            return NoContent(); 
        }

    }
}
