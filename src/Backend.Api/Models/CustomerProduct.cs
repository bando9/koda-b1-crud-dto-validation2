namespace Backend.Api.Models;

public sealed record CustomerProduct(
    string ProductCode, 
    string Name, 
    decimal Price,
    string Category, 
    int Stock, 
    bool IsActive
);