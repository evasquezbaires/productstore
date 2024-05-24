using AutoMapper;
using MediatR;
using ProductStore.Api.Domain.Contracts;
using ProductStore.Api.Domain.Entities;
using ProductStore.Api.Domain.Exceptions;
using ProductStore.Api.Model;

namespace ProductStore.Api.Domain.Handlers
{
    /// <summary>
    /// Class to operate with the Update command of existing products
    /// </summary>
    public class ModifyProductCommandHandler : IRequestHandler<ProductUpdate>
    {
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ModifyProductCommandHandler(IProductRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(ProductUpdate request, CancellationToken cancellationToken)
        {
            try
            {
                var existingProduct = await _repository.Get(request.Id);
                if (existingProduct is null)
                {
                    throw new DomainNotFoundException(nameof(Product), request.Id);
                }

                _mapper.Map(request, existingProduct);
                existingProduct.UpdatedDate = DateTime.UtcNow;

                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                throw new DomainException(ex.Message);
            }

            return;
        }
    }
}
