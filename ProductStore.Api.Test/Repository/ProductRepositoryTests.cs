using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;
using ProductStore.Api.Domain.Entities;
using ProductStore.Api.Repository;
using ProductStore.Api.Test.Util;
using System.Diagnostics.CodeAnalysis;

namespace ProductStore.Api.Test.Repository
{
    [ExcludeFromCodeCoverage]
    public class ProductRepositoryTests
    {
        private readonly Mock<DbSet<Product>> _dbSetMock;
        private readonly Mock<DatabaseContext> _dbContextMock;
        private readonly ProductRepository _productRepository;
        private readonly Guid _productId;

        public ProductRepositoryTests()
        {
            // Set up the DbSet mock
            _productId = Guid.NewGuid();
            var data = FakeEntities.GetFakeProducts(1).AsQueryable();
            _dbSetMock = CreateDbSetMock(data);

            // Set up the DbContext mock
            _dbContextMock = new Mock<DatabaseContext>();
            _dbContextMock.Setup(db => db.Set<Product>()).Returns(_dbSetMock.Object);

            // Initialize the repository
            _productRepository = new ProductRepository(_dbContextMock.Object);
        }

        [Fact]
        public async Task AddProduct_ShouldAddProduct()
        {
            // Arrange
            var product = FakeEntities.GetFakeProduct();

            _dbSetMock.Setup(set => set.AddAsync(product, It.IsAny<CancellationToken>()))
                .Returns<Product, CancellationToken>((e, c) =>
                {
                    var stateManagerMock = new Mock<IStateManager>();
                    var entityTypeMock = new Mock<IRuntimeEntityType>();
                    entityTypeMock
                        .SetupGet(_ => _.EmptyShadowValuesFactory)
                        .Returns(() => new Mock<ISnapshot>().Object);
                    entityTypeMock
                        .SetupGet(_ => _.Counts)
                        .Returns(new PropertyCounts(1, 1, 1, 1, 1, 1));
                    entityTypeMock
                        .Setup(_ => _.GetProperties())
                        .Returns(Enumerable.Empty<IProperty>());
                    var internalEntity = new InternalEntityEntry(stateManagerMock.Object,
                        entityTypeMock.Object, e);
                    var entry = new EntityEntry<Product>(internalEntity);
                    return ValueTask.FromResult(entry);
                });

            // Act
            var result = await _productRepository.AddProduct(product);

            // Assert
            Assert.Equal(product, result);
            _dbSetMock.Verify(set => set.AddAsync(product, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task AddProducts_ShouldAddProducts()
        {
            // Arrange
            var products = FakeEntities.GetFakeProducts(3);

            // Act
            await _productRepository.AddProducts(products);

            // Assert
            _dbSetMock.Verify(set => set.AddRangeAsync(products, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllProducts()
        {
            // Arrange

            // Act
            var result = await _productRepository.GetAll();

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void Remove_ShouldRemoveProduct()
        {
            // Arrange
            var product = FakeEntities.GetFakeProduct();

            // Act
            _productRepository.Remove(product);

            // Assert
            _dbSetMock.Verify(set => set.Remove(product), Times.Once);
        }

        private Mock<DbSet<T>> CreateDbSetMock<T>(IQueryable<T> data) where T : class
        {
            var dbSetMock = new Mock<DbSet<T>>();

            dbSetMock.As<IAsyncEnumerable<T>>()
                .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new TestAsyncEnumerator<T>(data.GetEnumerator()));

            dbSetMock.As<IQueryable<T>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<T>(data.Provider));

            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return dbSetMock;
        }
    }
}
