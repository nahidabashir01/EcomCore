using AppDbContext.DBContext;
using AppDbContext.Repository.IRepository;
using AppDbContext.Repository;
using Microsoft.EntityFrameworkCore;
using ServiceRespnse.Repository.IRepository;
using ServiceRespnse.Repository;
using UserMicroservice.Models;
using UserMicroservice.Dtos.Request.RequestValidator;
using FluentValidation.AspNetCore;
using FluentValidation;

namespace UserMicroservice.Registrations
{
    public static class ValidatorRegistration
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<UserRegistrationDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<UserLoginDtoValidator>();

            return services;
        }
    }
}
