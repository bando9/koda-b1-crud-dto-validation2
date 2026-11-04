using Backend.Api.Data;
using Backend.Api.Interfaces;
using Backend.Api.Repositories;
using Backend.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();
// builder.Services.AddSingleton<InMemoryProductCatalog>();

// builder.Services.AddSingleton<IProductRepository,InMemoryProductRepository>();
// builder.Services.AddSingleton<IProductService,ProductService>();

// * Repository
builder.Services.AddScoped<IProductRepository, SqlProductRepository>();

// * Service
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.MapControllers();

app.Run();
