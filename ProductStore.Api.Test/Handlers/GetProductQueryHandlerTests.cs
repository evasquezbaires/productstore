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
    public class GetProductQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ProductExists_ReturnsProductRead()
        {
            // Arrange
            var product = FakeEntities.GetFakeProduct();
            var productQuery = FakeModels.GetFakeProductQuery(product.Id);
            
            var discount = 10;
            var statusName = "Active";

            var repositoryMock = new Mock<IProductRepository>();
            repositoryMock.Setup(repo => repo.Get(productQuery.Id)).ReturnsAsync(product);

            var discountClientMock = new Mock<IDiscountClientService>();
            discountClientMock.Setup(client => client.GetDiscount(productQuery.Id)).ReturnsAsync(discount);

            var cacheServiceMock = new Mock<ICacheService>();
            cacheServiceMock.Setup(cache => cache.CheckCache(product.StatusCode.ToString())).ReturnsAsync(statusName);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<ProductRead>(product)).Returns(new ProductRead { Name = product.Name, Price = product.Price });

            var handler = new GetProductQueryHandler(repositoryMock.Object, discountClientMock.Object, cacheServiceMock.Object, mapperMock.Object);

            // Act
            var result = await handler.Handle(productQuery, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(product.Name, result.Name);
            Assert.Equal(product.Price, result.Price);
            Assert.Equal(discount, result.Discount);
            Assert.Equal(product.Price * (100 - discount) / 100, result.FinalPrice);
            Assert.Equal(statusName, result.StatusName);
        }

        [Fact]
        public async Task Handle_ProductNotFound_ThrowsDomainNotFoundException()
        {
            // Arrange
            var productQuery = FakeModels.GetFakeProductQuery();

            var repositoryMock = new Mock<IProductRepository>();
            repositoryMock.Setup(repo => repo.Get(productQuery.Id)).ReturnsAsync((Product)null);

            var discountClientMock = new Mock<IDiscountClientService>();
            var cacheServiceMock = new Mock<ICacheService>();
            var mapperMock = new Mock<IMapper>();

            var handler = new GetProductQueryHandler(repositoryMock.Object, discountClientMock.Object, cacheServiceMock.Object, mapperMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => handler.Handle(productQuery, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ExceptionThrown_ThrowsDomainException()
        {
            // Arrange
            var productQuery = FakeModels.GetFakeProductQuery();
            var errorMessage = "An error occurred.";

            var repositoryMock = new Mock<IProductRepository>();
            repositoryMock.Setup(repo => repo.Get(productQuery.Id)).ThrowsAsync(new Exception(errorMessage));

            var discountClientMock = new Mock<IDiscountClientService>();
            var cacheServiceMock = new Mock<ICacheService>();
            var mapperMock = new Mock<IMapper>();

            var handler = new GetProductQueryHandler(repositoryMock.Object, discountClientMock.Object, cacheServiceMock.Object, mapperMock.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<DomainException>(() => handler.Handle(productQuery, CancellationToken.None));
            Assert.Equal(errorMessage, exception.Message);
        }
    }
}
