using MediatR;
using ProductMicroservice.Dtos.Category;
using ServiceRespnse.Models;

namespace ProductMicroservice.Queries
{
    public class GetAllCategoriesQuery : IRequest<ResponseDto<List<CategoryDto>>>
    {
    }

}
