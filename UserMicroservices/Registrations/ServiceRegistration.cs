using AppDbContext.Repository.IRepository;
using AppDbContext.Repository;
using ServiceRespnse.Repository.IRepository;
using ServiceRespnse.Repository;
using UserMicroservice.Dtos.Request;
using UserMicroservice.Models;
using FluentValidation;
using AppDbContext.DBContext;
using Microsoft.EntityFrameworkCore;
using UserMicroservice.Dtos.Request.RequestValidator;
using Microsoft.AspNetCore.Identity;

namespace UserMicroservice.Registrations
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GenericDbContext<User>>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IResponseRepository, ResponseRepository>();

            return services;
        }
    }
}
