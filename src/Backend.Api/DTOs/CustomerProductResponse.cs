using Backend.Api.Models;

namespace Backend.Api.DTOs;

public sealed record CustomerProductResponse(
    string ProductCode, string Name, decimal Price,
    string Category, int Stock, bool IsActive)
{
    public static CustomerProductResponse FromModel(CustomerProduct p)
        => new(p.ProductCode, p.Name, p.Price, p.Category, p.Stock, p.IsActive);
}