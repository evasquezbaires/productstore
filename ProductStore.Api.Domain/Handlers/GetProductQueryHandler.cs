using AutoMapper;
using MediatR;
using ProductStore.Api.Domain.Contracts;
using ProductStore.Api.Domain.Entities;
using ProductStore.Api.Domain.Exceptions;
using ProductStore.Api.Model;

namespace ProductStore.Api.Domain.Handlers
{
    /// <summary>
    /// Class to get the information of existing product
    /// </summary>
    public class GetProductQueryHandler : IRequestHandler<ProductQuery, ProductRead>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public GetProductQueryHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProductRead> Handle(ProductQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var existingProduct = await _repository.Get(request.Id);
                if (existingProduct is null)
                {
                    throw new DomainNotFoundException(nameof(Product), request.Id);
                }

                var productMapped = _mapper.Map<ProductRead>(existingProduct);
                return productMapped;
            }
            catch (Exception ex)
            {
                throw new DomainException(ex.Message);
            }
        }
    }
}
