using Backend.Api.DTOs;
using Backend.Api.Interfaces;
using Backend.Api.Models;
using Microsoft.AspNetCore.Mvc;


namespace Backend.Api.Controllers;

[ApiController]
[Route("api/v1/products")]

public sealed class ProductController : ControllerBase
{
    private readonly IProductService _service;

    public ProductController(IProductService service)
    {
        _service=service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ProductResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetAll()
    {
        // var products = await _service.Get
        return Ok(_service.GetAll());
    }

    // GET Product by productCode
    [HttpGet("{productCode}")]
    [ProducesResponseType(typeof(ProductResponse),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status404NotFound)]
    public ActionResult<ProductResponse> GetByCode(
        [FromRoute] string productCode
    )
    {
        var product = _service.GetByCode(productCode);

        return product is null ? Problem(statusCode: StatusCodes.Status404NotFound, title: $"Sorry, product not found"):Ok(product);
    }


    [HttpPost]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<ProductResponse> PostProduct([FromBody] CreateProduct request)
    {

        try
        {
            var product = _service.Create(request);

            if(product is null)
            {
                // 409 Conflict
                return Problem(statusCode:StatusCodes.Status409Conflict, title: "Product already exist");
            }

            // 201 created
            return CreatedAtAction(
                nameof(GetByCode),
                new {productCode = product.ProductCode},
                product
            );
        }
        catch (ArgumentException e)
        {
            return Problem(
                statusCode:StatusCodes.Status400BadRequest,title:"Invalid product data!",detail:e.Message
            );
            
        }

    }

    // Delete by prodcutCode 
    [HttpDelete("{productCode}")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public IActionResult Delete([FromRoute]string productCode)
    {
        return _service.Delete(productCode) ? NoContent() : Problem(
            statusCode: StatusCodes.Status404NotFound,
            title: "Product not found",
            detail: $"No product was found for code {productCode}"
        );
    }
    

    [HttpPut("{productCode}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public ActionResult<ProductResponse> Update([FromRoute] string productCode, [FromBody] UpdateProductRequest request)
    {
        try
        {
            var existingCode = _service.Update(productCode, request);

            return existingCode is null ? Problem(statusCode: StatusCodes.Status404NotFound,title: $"product with {productCode} code not found.") : Ok(existingCode);
        }
        catch (ArgumentException e)
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title:"Invalid product data",
                detail: e.Message
            );
            
        }
    }



    

}