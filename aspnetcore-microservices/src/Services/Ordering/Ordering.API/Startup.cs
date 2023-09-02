
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Ordering.API.Extensions;
using Ordering.Application;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Mappings;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Repository;

namespace Ordering.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddApplicationServices2();
            services.AddInfrastructureServices(Configuration);
            services.AddConfigurationEmailSettings(Configuration);

            // MassTransit-RabbitMQ Configuration
            services.ConfigureMassTransit(Configuration);
            services.AddAutoMapper(typeof(Startup));
            //services.AddScoped<IOrderRepository, OrderRepository>();
            //services.AddScoped<IMedia, OrderRepository>();
            services.AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile()));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ordering.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ordering.API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                
            });
        }
    }
}
