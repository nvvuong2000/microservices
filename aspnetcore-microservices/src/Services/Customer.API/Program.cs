using Microsoft.EntityFrameworkCore;
using Customer.API.Persistance;
using Constracts.Common.Interfaces;
using Infrastructure.Common;
using Customer.API.Services.Interfaces;
using Customer.API.Services;
using Customer.API.Repository;
using Customer.API.Repository.Interfaces;
using Customer.API.Controllers;

var builder = WebApplication.CreateBuilder(args);
//builder.Host.UseSerilog(Serilogger.Configure);
// Add services to the container.

builder.Services.AddControllers();
var connectionString = builder.Configuration.GetConnectionString(name: "DefaultConnectionString");

builder.Services.AddDbContext<CustomerContext>(
    options => options.UseNpgsql(connectionString));

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>()
                .AddScoped(typeof(IRepositoryBaseAsync<,,>), typeof(RepositoryBaseAsync<,,>))
                .AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>))
                .AddScoped<ICustomerServices, CustomerServices>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapCustomerAPI();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.SeedCustomerData().Run();
