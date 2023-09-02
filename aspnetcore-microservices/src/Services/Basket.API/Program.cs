using Basket.API;
using Basket.API.Extensions;
using Basket.API.Repository;
using Basket.API.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile()));
var redisConnectString = builder.Configuration.GetSection("CacheSettings:ConnectionString").Value;
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisConnectString;
});
//builder.Services.AddConfigurationEventBusSettings(builder.Configuration);
// configure mass transit
builder.Services.ConfigureMassTransit(builder.Configuration);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
