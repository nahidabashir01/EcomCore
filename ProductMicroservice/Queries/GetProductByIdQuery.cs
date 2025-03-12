using MediatR;
using ProductMicroservice.Dtos;

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
