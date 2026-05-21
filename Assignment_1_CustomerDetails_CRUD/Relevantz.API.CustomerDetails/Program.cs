using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Relevantz.API.CustomerDetails.Constants;
using Relevantz.API.CustomerDetails.Interfaces;
using Relevantz.API.CustomerDetails.Repositories;
using Relevantz.API.CustomerDetails.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Register application services
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(CustomerConstants.SwaggerVersion, new OpenApiInfo
    {
        Title = CustomerConstants.SwaggerTitle,
        Version = CustomerConstants.SwaggerVersion,
        Description = CustomerConstants.SwaggerDescription
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{CustomerConstants.SwaggerTitle} {CustomerConstants.SwaggerVersion}");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

// Make Program class accessible to integration tests
public partial class Program { }
