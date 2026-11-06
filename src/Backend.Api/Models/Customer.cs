
namespace Backend.Api.Models;

public class Customer(string name, string email, string phone, string address)
{
    public string Name { get; private set; } = name;
    public string Email {get;private set;} = email;
    public string Phone {get;private set;} = phone;
    public string Address {get;private set;} = address;
}