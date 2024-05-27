using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductStore.Api.Model;
using Swashbuckle.AspNetCore.Annotations;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace ProductStore.Api.Controllers
{
    /// <summary>
    /// Controls operation over stock items on the Store.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class StoreController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor declaration
        /// </summary>
        /// <param name="mediator">Mediator instance to handle requests</param>
        public StoreController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new Product and persist it into the repository.
        /// </summary>
        [HttpPost("SaveProduct")]
        [SwaggerResponse(201, "The request was validated and processed successfully.")]
        [SwaggerResponse(400, "The request has one or more validation issues that can be resolved by the requester.")]
        [SwaggerResponse(500, "The request raised an internal server error that should be analyzed and resolved by the developers.")]
        public async Task<IActionResult> Insert([FromBody] ProductWrite request)
        {
            var resultId = await _mediator.Send(request);
            return StatusCode((int)HttpStatusCode.Created, resultId);
        }

        /// <summary>
        /// Updates an existing Product and persist it into the repository.
        /// </summary>
        [HttpPut("UpdateProduct/{id}")]
        [SwaggerResponse(200, "The request was validated and processed successfully.")]
        [SwaggerResponse(400, "The request has one or more validation issues that can be resolved by the requester.")]
        [SwaggerResponse(500, "The request raised an internal server error that should be analyzed and resolved by the developers.")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ProductUpdate request)
        {
            request.Id = id;
            var resultId = await _mediator.Send(request);
            return Ok(resultId);
        }

        /// <summary>
        /// Lists a single Product filtered by Id.
        /// </summary>
        [HttpGet("GetProduct/{id}")]
        [SwaggerResponse(200, "The request was validated and processed successfully.", typeof(ProductRead))]
        [SwaggerResponse(400, "The request has one or more validation issues that can be resolved by the requester.")]
        [SwaggerResponse(500, "The request raised an internal server error that should be analyzed and resolved by the developers.")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var request = new ProductQuery { Id = id };
            var model = await _mediator.Send(request);

            return Ok(model);
        }
    }
}
