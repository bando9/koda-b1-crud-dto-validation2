using Backend.Api.DTOs;
using Backend.Api.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Backend.Api.Interfaces;

public interface IProductService
{
    IReadOnlyList<ProductResponse> GetAll();
    ProductResponse? GetByCode(string productCode);
    ProductResponse? Create(CreateProduct request);
    ProductResponse? Update(string productCode, UpdateProductRequest request);

    bool Delete(string productCode);
    ViewDataDictionary Create(Product product);
}