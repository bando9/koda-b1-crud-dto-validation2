using Backend.Api.DTOs;
using Backend.Api.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Backend.Api.Interfaces;

public interface IProductService
{
    Task<IReadOnlyList<ProductResponse>> GetAllAsync(CancellationToken cancellationToken);
    Task<ProductResponse?> GetByCodeAsync(string productCode, CancellationToken cancellationToken);
    Task<ProductResponse?> CreateAsync(ProductRequest request, CancellationToken cancellationToken );
    Task<ProductResponse?> UpdateAsync(string productCode, UpdateProductRequest request, CancellationToken cancellationToken);

    Task<bool> DeleteAsync(string productCode, CancellationToken cancellationToken);
    // ViewDataDictionary Create(Product product);
}