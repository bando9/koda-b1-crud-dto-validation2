using Backend.Api.DTOs;
using Backend.Api.Interfaces;
using Backend.Api.Models;
using Backend.Api.Services;
using Microsoft.AspNetCore.Mvc;


namespace Backend.Api.Controllers;

[ApiController]
[Route("api/v1/customers")]

public sealed class CustomerController : ControllerBase
{
    private readonly ICustomerService _service;

    public CustomerController(ICustomerService service)
    {
        _service=service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<CustomerResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<Customer>>> GetAll(CancellationToken cancellationToken)
    {
        var customers = await _service.GetAllAsync(cancellationToken);

        return Ok(customers);
    }

    // GET Product by productCode
    [HttpGet("{email}")]
    [ProducesResponseType(typeof(CustomerResponse),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerResponse>> GetByEmail(
        [FromRoute] string email,CancellationToken cancellationToken
    )
    {
        var customer = await _service.GetByEmailAsync(email,cancellationToken);

        return customer is null ? Problem(statusCode: StatusCodes.Status404NotFound, title: $"Sorry, customer not yet registered"):Ok(customer);
    }


    // POST 
    [HttpPost]
    [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CustomerResponse>> PostCustomer([FromBody] CustomerRequest request,CancellationToken cancellationToken)
    {

        try
        {

            var customer = await _service.CreateAsync(request,cancellationToken);

            if(customer is null)
            {
                // 409 Conflict
                return Problem(statusCode:StatusCodes.Status409Conflict, title: "Customer already exist");
            }

            // 201 created
            return CreatedAtAction(
                nameof(GetByEmail),
                new {email = customer.Email},
                customer
            );
        }
        catch (ArgumentException e)
        {
            return Problem(
                statusCode:StatusCodes.Status400BadRequest,title:"Invalid customer data!",detail:e.Message
            );
            
        }

    }

    // Delete by email 
    [HttpDelete("{email}")]
    [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute]string email,CancellationToken cancellationToken)
    {
        var deleted = await _service.DeleteAsync(email, cancellationToken);

        return deleted ? NoContent() : Problem(
            statusCode: StatusCodes.Status404NotFound,
            title: "Product not found",
            detail: $"No product was found for email {email}");
    }
    

    // Update
    [HttpPut("{email}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CustomerResponse>> Update([FromRoute] string email, [FromBody] UpdateCustomerRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var updated = await _service.UpdateAsync(email, request,cancellationToken);

            return updated is null ? Problem(statusCode: StatusCodes.Status404NotFound,title: $"customer with {email} email not found.") : Ok(updated);
        }
        catch (ArgumentException e)
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title:"Invalid customer data",
                detail: e.Message
            );
            
        }
    }



    

}