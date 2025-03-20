using MediatR;
using ServiceRespnse.Models;

namespace ProductMicroservice.Commands
{
    public class DeleteProductCommand : IRequest<ResponseDto<bool>>
    {
        public Guid ProductId { get; set; }
    }
}
