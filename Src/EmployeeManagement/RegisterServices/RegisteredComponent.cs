using EmployeeManagement.Contracts;
using EmployeeManagement.Services;
using Microsoft.Extensions.Options;

namespace EmployeeManagement.RegisterServices
{
    public static class RegisteredComponent
    {
        public static IServiceCollection AddDepandancies(this IServiceCollection services)
        {
            services.AddTransient<IEmployeeServices, EmployeeServices>();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", builder =>
                {
                    
                    builder.AllowAnyOrigin()  
                           .AllowAnyMethod() 
                           .AllowAnyHeader();  
                });
            });
            return services;

        }

        public static WebApplication AddMiddleware(this WebApplication applicationBuilder)
        {
            applicationBuilder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
            applicationBuilder.UseCors("AllowAllOrigins");  
            return applicationBuilder;      
        }
    }
}
