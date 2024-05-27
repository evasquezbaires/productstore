using LazyCache;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using ProductStore.Api.Cache;
using System.Diagnostics.CodeAnalysis;

namespace ProductStore.Api.Test.Cache
{
    [ExcludeFromCodeCoverage]
    public class StatusCacheServiceTests
    {
        private readonly Mock<IAppCache> _cacheMock;
        private readonly StatusCacheService _statusCacheService;

        public StatusCacheServiceTests()
        {
            _cacheMock = new Mock<IAppCache>();
            _statusCacheService = new StatusCacheService(_cacheMock.Object);
        }

        [Fact]
        public async Task Get_ShouldReturnCachedValue_WhenKeyExists()
        {
            // Arrange
            var key = "0";
            var expectedValue = "TestValue";
            _cacheMock.Setup(c => c.GetAsync<string>(key)).ReturnsAsync(expectedValue);

            // Act
            var result = await _statusCacheService.Get(key);

            // Assert
            Assert.Equal(expectedValue, result);
            _cacheMock.Verify(c => c.GetAsync<string>(key), Times.Once);
        }

        [Fact]
        public async Task Save_ShouldAddValueToCache()
        {
            // Arrange
            var key = "1";
            var value = "Active";
            _cacheMock.Setup(c => c.Add<string>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<MemoryCacheEntryOptions>()));

            // Act
            await _statusCacheService.Save(key, value);

            // Assert
            _cacheMock.Verify(c => c.Add<string>(key, value, null), Times.Once);
        }

        //[Fact]
        //public async Task CheckCache_ShouldReturnCachedValue_WhenKeyExists()
        //{
        //    // Arrange
        //    var key = "1";
        //    var expectedValue = "Active";
        //    _cacheMock.Setup(c => c.GetOrAddAsync<string>(key, It.IsAny<Func<Task<string>>>())).ReturnsAsync(expectedValue);

        //    // Act
        //    var result = await _statusCacheService.CheckCache(key);

        //    // Assert
        //    Assert.Equal(expectedValue, result);
        //    _cacheMock.Verify(c => c.GetOrAddAsync<string>(key, It.IsAny<Func<Task<string>>>()), Times.Once);
        //}

        //[Fact]
        //public async Task CheckCache_ShouldInitializeCache_WhenKeyDoesNotExist()
        //{
        //    // Arrange
        //    var key = "0";
        //    var expectedValue = "Inactive";
        //    _cacheMock.Setup(c => c.GetOrAddAsync(key, It.IsAny<Func<Task<string>>>())).Returns<string, Func<Task<string>>>(async (k, f) => await f());

        //    // Act
        //    var result = await _statusCacheService.CheckCache(key);

        //    // Assert
        //    Assert.Equal(expectedValue, result);
        //    _cacheMock.Verify(c => c.GetOrAddAsync<string>(key, It.IsAny<Func<Task<string>>>()), Times.Once);
        //}
    }
}
