using Backend.Api.Models;

namespace Backend.Api.Data;

public sealed class InMemoryProductCatalog
{
    private readonly object _sync = new();
    private readonly List<Product> _products = [];

    public IReadOnlyList<Product> GetAll()
    {
        lock (_sync)
        {
            
        return _products.ToArray();
        }
    }

    public Product? GetByCode(string productCode)
    {
        lock (_sync)
        {
            
        return _products.FirstOrDefault(product => string.Equals(product.ProductCode,productCode.Trim(), StringComparison.OrdinalIgnoreCase));
        }
    }

    public bool TryAddPoduct(Product product)
    {
        lock (_sync)
        {
            if (_products.Any(p => String.Equals(p.ProductCode, product.ProductCode, StringComparison.OrdinalIgnoreCase)))
            {
                // if product already exist
                return false;
            }

            _products.Add(product);
            return true;

        }
    }

    public bool TryRemoveProduct(string productCode)
    {
        lock (_sync)
        {
            var index = _products.FindIndex(product => String.Equals(product.ProductCode, productCode.Trim(), StringComparison.OrdinalIgnoreCase));

            // if index not found
            if(index < 0 ) return false;

            _products.RemoveAt(index);
            return true;
        }
    }

    public bool TryUpdateProduct(Product product)
    {
        lock (_sync)
        {
            var index = _products.FindIndex(p => String.Equals(p.ProductCode, product.ProductCode.Trim(), StringComparison.OrdinalIgnoreCase));

            // if index not found
            if(index < 0 ) return false;

            _products[index] = product;
            return true;
        }        
    }
}