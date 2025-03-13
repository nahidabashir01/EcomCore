using MediatR;
using ProductMicroservice.Dtos;
using ProductMicroservice.Dtos.Product;

namespace ProductMicroservice.Queries
{
    public class GetProductByIdQuery: IRequest<ProductReadDto>
    {
        public Guid Id { get; set; }

        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
