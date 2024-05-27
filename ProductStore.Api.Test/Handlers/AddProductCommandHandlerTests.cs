using AutoMapper;
using Moq;
using ProductStore.Api.Domain.Contracts;
using ProductStore.Api.Domain.Entities;
using ProductStore.Api.Domain.Exceptions;
using ProductStore.Api.Domain.Handlers;
using ProductStore.Api.Test.Util;
using System.Diagnostics.CodeAnalysis;

namespace ProductStore.Api.Test.Handlers
{
    [ExcludeFromCodeCoverage]
    public class AddProductCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidProduct_AddsProductAndCommitsUnitOfWork()
        {
            // Arrange
            var productWrite = FakeModels.GetFakeProductWrite();

            var repositoryMock = new Mock<IProductRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();

            var handler = new AddProductCommandHandler(repositoryMock.Object, unitOfWorkMock.Object, mapperMock.Object);

            // Act
            await handler.Handle(productWrite, CancellationToken.None);

            // Assert
            repositoryMock.Verify(repo => repo.AddProduct(It.IsAny<Product>(), default), Times.Once);
            unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_ExceptionThrown_RollsBackUnitOfWorkAndThrowsDomainException()
        {
            // Arrange
            var productWrite = FakeModels.GetFakeProductWrite();
            var errorMessage = "An error occurred.";

            var repositoryMock = new Mock<IProductRepository>();
            repositoryMock.Setup(repo => repo.AddProduct(It.IsAny<Product>(), default)).ThrowsAsync(new Exception(errorMessage));

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();

            var handler = new AddProductCommandHandler(repositoryMock.Object, unitOfWorkMock.Object, mapperMock.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<DomainException>(() => handler.Handle(productWrite, CancellationToken.None));
            Assert.Equal(errorMessage, exception.Message);

            unitOfWorkMock.Verify(uow => uow.Rollback(), Times.Once);
        }
    }
}
