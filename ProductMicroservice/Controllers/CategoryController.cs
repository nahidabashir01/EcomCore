using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductMicroservice.Commands;
using ProductMicroservice.Dtos.Category;
using ProductMicroservice.Queries;

namespace CategoryMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto categoryCreateDto)
        {
            var createCategoryCommand = new CreateCategoryCommand { CreateCategoryDto = categoryCreateDto };

            var response = await _mediator.Send(createCategoryCommand);

            if (response.IsSuccess)
                return CreatedAtAction(nameof(CreateCategory), new { id = response.Data }, response);

            return Conflict(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryDto categoryUpdateDto)
        {
            var updateCategoryCommand = new UpdateCategoryCommand { UpdateCategoryDto = categoryUpdateDto };

            var response = await _mediator.Send(updateCategoryCommand);

            if (response.IsSuccess)
                return Ok(response);

            return Conflict(response);
        }

        [HttpDelete("delete/{id:guid}")]
        public async Task<IActionResult> DeleteCategoryById(DeleteCategoryDto deleteCategoryDto)
        {
            var deleteCategoryCommand = new DeleteCategoryByIdCommand { DeleteCategoryDto = deleteCategoryDto };

            var response = await _mediator.Send(deleteCategoryCommand);

            if (response.IsSuccess)
                return Ok(response);

            return NotFound(response);
        }

        [HttpGet("get-all-categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var query = new GetAllCategoriesQuery();
            var response = await _mediator.Send(query);

            if (response.IsSuccess)
                return Ok(response);

            return NotFound(response);
        }
    }
}
