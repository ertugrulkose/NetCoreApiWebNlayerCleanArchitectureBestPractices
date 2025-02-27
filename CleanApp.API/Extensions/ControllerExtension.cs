using CleanApp.API.Filters;

namespace CleanApp.API.Extensions
{
    public static class ControllerExtension
    {
        public static IServiceCollection AddControllersWithFiltersExt(this IServiceCollection services)
        {
            services.AddOpenApi();

            services.AddScoped(typeof(NotFoundFilter<,>));

            services.AddControllers(options =>
            {
                options.Filters.Add<FluentValidationFilter>();
                // Turn off default nullable reference types control
                // options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            });
            return services;
        }
    }
}
