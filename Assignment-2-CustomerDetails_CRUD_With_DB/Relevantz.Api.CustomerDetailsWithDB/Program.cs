using Microsoft.EntityFrameworkCore;
using Relevantz.Api.CustomerDetailsWithDB.Data.Context;
using Relevantz.Api.CustomerDetailsWithDB.Data.Interfaces;
using Relevantz.Api.CustomerDetailsWithDB.Data.Repositories;
using Relevantz.Api.CustomerDetailsWithDB.Interfaces;
using Relevantz.Api.CustomerDetailsWithDB.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CustomerDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerAddressRepository, CustomerAddressRepository>();
builder.Services.AddScoped<ICustomerBusinessProfileRepository, CustomerBusinessProfileRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<ICustomerTagRepository, CustomerTagRepository>();

// Register services
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerAddressService, CustomerAddressService>();
builder.Services.AddScoped<ICustomerBusinessProfileService, CustomerBusinessProfileService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<ICustomerTagService, CustomerTagService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CustomerDbContext>();
    db.Database.EnsureCreated();
}

app.UseMiddleware<Relevantz.Api.CustomerDetailsWithDB.Middleware.GlobalExceptionHandlerMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

namespace Relevantz.Api.CustomerDetailsWithDB
{
    public partial class Program { }
}
