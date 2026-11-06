
using Backend.Api.DTOs;
using Backend.Api.Models;
using Backend.Api.Repositories;

namespace Backend.Api.Services;

public sealed class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;

    public CustomerService(ICustomerRepository repository)
    {
        _repository=repository;
    }

    public async Task<CustomerResponse?> CreateAsync(CustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = new Customer(request.Name, request.Email, request.Phone, request.Address);

        var created = await _repository.TryAddAsync(customer, cancellationToken);

        return created ? CustomerResponse.FromModel(customer) : null;
 
    }

    public Task<bool> DeleteAsync(string email, CancellationToken cancellationToken)
    {
        if(string.IsNullOrWhiteSpace(email) || email.Trim().Length > 30)
        {
            return Task.FromResult(false);
        }

        return _repository.TryDeleteAsync(email,cancellationToken);
    }

    public async Task<IReadOnlyList<CustomerResponse>> GetAllAsync(CancellationToken cancellationToken)
    {
        var customers = await _repository.GetAllAsync(cancellationToken);

        return customers.Select(CustomerResponse.FromModel).ToArray();
    }

    public async Task<CustomerResponse?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        if(string.IsNullOrWhiteSpace(email) || email.Trim().Length > 30)
        {
            return null;
        }
        
        var customer = await _repository.GetByEmailAsync(email, cancellationToken);
        return customer is null ? null : CustomerResponse.FromModel(customer);
    }

    public async Task<CustomerResponse?> UpdateAsync(string email, UpdateCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = new Customer(request.Name,email, request.Phone, request.Address);

        var updated = await _repository.TryUpdateAsync(customer, cancellationToken);

        return updated ? CustomerResponse.FromModel(customer) : null;
    }
}