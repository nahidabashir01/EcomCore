using MediatR;
using ProductMicroservice.Dtos.Category;
using ProductMicroservice.Dtos.Product;
using ServiceRespnse.Models;

namespace ProductMicroservice.Queries
{
    public class GetAllProductsQuery : IRequest<ResponseDto<List<ProductDto>>>
    {
    }

}
