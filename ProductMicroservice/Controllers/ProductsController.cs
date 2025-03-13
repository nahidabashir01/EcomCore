using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductMicroservice.Commands;
using ProductMicroservice.Dtos;
using ProductMicroservice.Dtos.Product;

namespace ProductMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct([FromForm] ProductCreateDto productCreateDto)
        {
            var createProductcommand = new CreateProductCommand() { ProductCreateDto = productCreateDto };

            var response = await _mediator.Send(createProductcommand);

            if (response.IsSuccess)
                return CreatedAtAction(nameof(CreateProduct), new { id = response.Data }, response);

            return Conflict(response);
        }

    }
}
