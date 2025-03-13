using AppDbContext.DBContext;
using AppDbContext.Repository;
using AppDbContext.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using ProductMicroservice.Models;
using ProductMicroservice.Services;
using ProductMicroservice.Services.IService;

namespace ProductMicroservice.Registrations
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddProductServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register DB Context
            services.AddDbContext<GenericDbContext<Product>>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            // Register Repositories
            services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();

            // Register Product Service
            services.AddScoped<IProductService, ProductService>();

            // Register Category Service
            services.AddScoped<ICategoryService, CategoryService>();

            // Register MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly));

            return services;
        }
    }
}
