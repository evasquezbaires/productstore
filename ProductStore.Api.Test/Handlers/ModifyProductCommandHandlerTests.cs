using AutoMapper;
using Moq;
using ProductStore.Api.Domain.Contracts;
using ProductStore.Api.Domain.Entities;
using ProductStore.Api.Domain.Exceptions;
using ProductStore.Api.Domain.Handlers;
using ProductStore.Api.Model;
using ProductStore.Api.Test.Util;
using System.Diagnostics.CodeAnalysis;

namespace ProductStore.Api.Test.Handlers
{
    [ExcludeFromCodeCoverage]
    public class ModifyProductCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ExistingProduct_ModifiesProductAndCommitsUnitOfWork()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var productUpdate = FakeModels.GetFakeProductUpdate(productId);
            var existingProduct = FakeEntities.GetFakeProduct(productId);

            var repositoryMock = new Mock<IProductRepository>();
            repositoryMock.Setup(repo => repo.Get(productId)).ReturnsAsync(existingProduct);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();

            var handler = new ModifyProductCommandHandler(repositoryMock.Object, unitOfWorkMock.Object, mapperMock.Object);

            // Act
            await handler.Handle(productUpdate, CancellationToken.None);

            // Assert
            repositoryMock.Verify(repo => repo.Get(productId), Times.Once);
            mapperMock.Verify(mapper => mapper.Map(productUpdate, existingProduct), Times.Once);
            unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_NonExistingProduct_ThrowsDomainNotFoundException()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var productUpdate = FakeModels.GetFakeProductUpdate(productId);

            var repositoryMock = new Mock<IProductRepository>();
            repositoryMock.Setup(repo => repo.Get(productId)).ReturnsAsync((Product)null);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();

            var handler = new ModifyProductCommandHandler(repositoryMock.Object, unitOfWorkMock.Object, mapperMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => handler.Handle(productUpdate, CancellationToken.None));
            unitOfWorkMock.Verify(uow => uow.Rollback(), Times.Once);
        }

        [Fact]
        public async Task Handle_ExceptionThrown_RollsBackUnitOfWorkAndThrowsDomainException()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var productUpdate = FakeModels.GetFakeProductUpdate(productId);
            var errorMessage = "An error occurred.";

            var existingProduct = new Product { Id = productId, Name = "Old Product", Price = 100 };

            var repositoryMock = new Mock<IProductRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();

            var handler = new ModifyProductCommandHandler(repositoryMock.Object, unitOfWorkMock.Object, mapperMock.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<DomainException>(() => handler.Handle(productUpdate, CancellationToken.None));
            unitOfWorkMock.Verify(uow => uow.Rollback(), Times.Once);
        }
    }
}
