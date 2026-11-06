using Backend.Api.Models;

namespace Backend.Api.Repositories;

public interface ICustomerRepository
{
    Task<IReadOnlyList<Customer>> GetAllAsync(CancellationToken cancellationToken);
    Task<Customer?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<bool> TryAddAsync(Customer customer, CancellationToken cancellationToken);
    Task<bool> TryUpdateAsync(Customer customer, CancellationToken cancellationToken);
    Task<bool> TryDeleteAsync(string email, CancellationToken cancellationToken);

    Task<IReadOnlyList<CustomerProduct>> GetProductsByEmailAsync(string email, CancellationToken cancellationToken);
    
}