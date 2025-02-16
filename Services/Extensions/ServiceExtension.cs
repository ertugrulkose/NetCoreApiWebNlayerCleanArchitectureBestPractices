using App.Services.Categories;
using App.Services.ExceptionHandlers;
using App.Services.Products;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace App.Services.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();

            // async validation için kapatılması gerekir
            services.AddFluentValidationAutoValidation();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // for automapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // for ExceptionHandler
            services.AddExceptionHandler<CriticalExceptionHandler>();
            services.AddExceptionHandler<GlobalExceptionHandler>();

            return services;
        }
    }
}
