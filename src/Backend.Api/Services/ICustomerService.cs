
using Backend.Api.DTOs;

namespace Backend.Api.Services;

public interface ICustomerService
{
    Task<IReadOnlyList<CustomerResponse>> GetAllAsync(CancellationToken cancellationToken);
    Task<CustomerResponse?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<CustomerResponse?> CreateAsync(CustomerRequest request, CancellationToken cancellationToken );
    Task<CustomerResponse?> UpdateAsync(string email, UpdateCustomerRequest request, CancellationToken cancellationToken);

    Task<bool> DeleteAsync(string email, CancellationToken cancellationToken);

    Task<IReadOnlyList<CustomerProductResponse>?> GetProductsAsync(string email, CancellationToken cancellationToken);
    
}