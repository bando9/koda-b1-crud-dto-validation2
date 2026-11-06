
using Backend.Api.Models;

namespace Backend.Api.DTOs;

public sealed record CustomerResponse(string Name, string Email, string Phone,string Address)
{
    public static CustomerResponse FromModel(Customer customer) => new(customer.Name,customer.Email,customer.Phone,customer.Address);
}