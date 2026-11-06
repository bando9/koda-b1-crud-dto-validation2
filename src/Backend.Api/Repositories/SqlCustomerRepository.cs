using System.Data;
using Backend.Api.Models;
using Microsoft.Data.SqlClient;

namespace Backend.Api.Repositories;

public sealed class SqlCustomerRepository : ICustomerRepository
{
    private readonly string _connectionString;

    public SqlCustomerRepository(IConfiguration configuration)
    {
        _connectionString=configuration.GetConnectionString("ecommercedb") ?? throw new InvalidOperationException("Connection string to ecommerce needed");
    }

    public async Task<IReadOnlyList<Customer>> GetAllAsync(CancellationToken cancellationToken)
    {
        const string sql = """
            SELECT Name, Email, Phone, Address FROM dbo.Customer ORDER BY Name DESC;
        """;

        List<Customer> customers = new();
        await using var connection = new SqlConnection(_connectionString);

        await connection.OpenAsync(cancellationToken);

        await using var command = new SqlCommand(sql, connection);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        while(await reader.ReadAsync(cancellationToken))
        {
            customers.Add(ReadCustomer(reader));
        }

        return customers;
    }
    

    public async Task<Customer?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        const string sql = """
            SELECT Name, Email, Phone, Addres FROM dbo.Customer WHERE Email = @email;
        """;

        await using var connection = new SqlConnection(_connectionString);

        await connection.OpenAsync(cancellationToken);

        await using var command = new SqlCommand(sql, connection);

        AddCustomerEmail(command, email);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        
        if(!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        return ReadCustomer(reader);

    }

    public async Task<bool> TryAddAsync(Customer customer, CancellationToken cancellationToken)
    {
        const string sql = """
            INSERT INTO dbo.Customer(Email, Name, Phone, Address) VALUES (@email, @name, @phone, @address);
        """;

        await using var connection = new SqlConnection(_connectionString);

        await connection.OpenAsync(cancellationToken);

        await using var command = new SqlCommand(sql, connection);

        AddCustomerParameter(command,customer);

        try
        {
            var affectedRow = await command.ExecuteNonQueryAsync(cancellationToken);

            return affectedRow == 1;
        }
        catch (SqlException e) when (e.Number is 2601 or 2627)
        {
            return false;
        } catch(SqlException e)
        {
            System.Console.WriteLine($"SQL error tidak terdufa: {e.Number} - {e.Message}");
            return false;
        }
    }

    public async Task<bool> TryDeleteAsync(string email, CancellationToken cancellationToken)
    {
        
        const string sql = """
            DELETE FROM dbo.Customer WHERE email=@email
        """;

        await using var connection = new SqlConnection(_connectionString);

        await connection.OpenAsync(cancellationToken);

        await using var command = new SqlCommand(sql,connection);

        AddCustomerEmail(command,email);

        var affectedRows = await command.ExecuteNonQueryAsync(cancellationToken);

        return affectedRows == 1;
    }

    public async Task<bool> TryUpdateAsync(Customer customer, CancellationToken cancellationToken)
    {
        const string sql = """
            UPDATE dbo.Customer SET
                Name = @name,
                Phone = @phone,
                Address = @address
            WHERE email = @email;
        """;

        await using var connection = new SqlConnection(_connectionString);

        await connection.OpenAsync(cancellationToken);

        await using var command = new SqlCommand(sql, connection);

        AddCustomerParameter(command,customer);

        var affectedRows = await command.ExecuteNonQueryAsync(cancellationToken);
        
        return affectedRows == 1;
    }


    // * ========== METHOD ==========

    private static void AddCustomerParameter(SqlCommand command, Customer customer)
    {
        AddCustomerEmail(command, customer.Email);

        command.Parameters.Add("@name", SqlDbType.NVarChar,200).Value = customer.Name;

        command.Parameters.Add("@phone", SqlDbType.NVarChar,150).Value = customer.Phone;

        command.Parameters.Add("@address", SqlDbType.NVarChar,500).Value = customer.Address;


    }

    private static void AddCustomerEmail(SqlCommand command, string email)
    {
        command.Parameters.Add("@email", SqlDbType.NVarChar,200).Value = email.Trim().ToUpperInvariant();
    }

    public static Customer ReadCustomer(SqlDataReader reader)
    {
        return new Customer(
                reader.GetString(0), 
                reader.GetString(1),
                reader.GetString(2),
                reader.GetString(3)
            );
        }
}