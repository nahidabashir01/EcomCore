using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductMicroservice.Commands;
using ProductMicroservice.Dtos.Product;
using ProductMicroservice.Queries;
using ServiceRespnse.Models;

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
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductDto createProductDto)
        {
            var createProductcommand = new CreateProductCommand() { CreateProductDto = createProductDto };

            var response = await _mediator.Send(createProductcommand);

            if (response.IsSuccess)
                return CreatedAtAction(nameof(CreateProduct), new { id = response.Data }, response);

            return Conflict(response);
        }

        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<ResponseDto<List<ProductDto>>>> GetAllProducts()
        {
            var query = new GetAllProductsQuery();

            var response = await _mediator.Send(query);

            if (response.IsSuccess)
                return Ok(response);

            return NotFound(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromForm] UpdateProductDto updateProductDto)
        {
            var updateProductCommand = new UpdateProductCommand { UpdateProductDto = updateProductDto };
            var response = await _mediator.Send(updateProductCommand);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _mediator.Send(new DeleteProductCommand { ProductId = id });

            return StatusCode(response.StatusCode, response);
        }


    }
}
