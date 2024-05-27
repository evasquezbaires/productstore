using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductStore.Api.Controllers;
using ProductStore.Api.Model;
using ProductStore.Api.Test.Util;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace ProductStore.Api.Test.Controllers
{
    [ExcludeFromCodeCoverage]
    public class StoreControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly StoreController _storeController;

        public StoreControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _storeController = new StoreController(_mediatorMock.Object);
        }

        [Fact]
        public async Task Insert_ValidProduct_ReturnsCreated()
        {
            // Arrange
            var productWrite = FakeModels.GetFakeProductWrite();
            _mediatorMock.Setup(mediator => mediator.Send(productWrite, default));

            // Act
            var result = await _storeController.Insert(productWrite);

            // Assert
            Assert.IsType<ObjectResult>(result);
            var statusCodeResult = (ObjectResult)result;
            Assert.Equal((int)HttpStatusCode.Created, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task Update_ExistingProduct_ReturnsOk()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var productUpdate = FakeModels.GetFakeProductUpdate(productId);
            _mediatorMock.Setup(mediator => mediator.Send(productUpdate, default));

            // Act
            var result = await _storeController.Update(productId, productUpdate);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetById_ExistingProduct_ReturnsOkWithModel()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var productRead = FakeModels.GetFakeProductRead();
            _mediatorMock.Setup(mediator => mediator.Send(It.IsAny<ProductQuery>(), default)).ReturnsAsync(productRead);

            // Act
            var result = await _storeController.GetById(productId);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okObjectResult.StatusCode);
            Assert.Equal(productRead, okObjectResult.Value);
        }
    }
}
