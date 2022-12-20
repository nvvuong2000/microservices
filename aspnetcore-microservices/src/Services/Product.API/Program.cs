using Serilog;
using Common.Logging;
using Product.API.Extensions;
using System.Configuration;
using Product.API.Persistance;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(Serilogger.Configure);

Log.Information("Start Product API");
Log.Error("This is the Privacy Page!");
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();



// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{

//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.MirgateDatabase<ProductContext>((context, _) =>
{
    ProductContextSeed.SeedProductAsync(context, Log.Logger).Wait();
})
.Run();
