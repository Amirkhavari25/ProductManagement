using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Features.Products.Commands.CreateProduct;
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

        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        //{
        //    var productId = await _mediator.Send(command);
        //    return CreatedAtAction(nameof(GetById), new { id = productId }, productId);
        //}

        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> GetAll()
        {
            var products = await _mediator.Send(new GetProductQuery());
            return Ok(products);
        }
    }
}
