﻿using AutoMapper;
using MediatR;
using ProductStore.Api.Domain.Contracts;
using ProductStore.Api.Domain.Entities;
using ProductStore.Api.Domain.Exceptions;
using ProductStore.Api.Model;

namespace ProductStore.Api.Domain.Handlers
{
    /// <summary>
    /// Class to operate with the Insert command of new products
    /// </summary>
    public class AddProductCommandHandler : IRequestHandler<ProductWrite>
    {
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddProductCommandHandler(IProductRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(ProductWrite request, CancellationToken cancellationToken)
        {
            try
            {
                var newProduct = _mapper.Map<Product>(request);

                await _repository.AddProduct(newProduct);

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
