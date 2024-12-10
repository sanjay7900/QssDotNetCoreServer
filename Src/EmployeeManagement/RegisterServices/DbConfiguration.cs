using EmployeeManagement.DB;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.RegisterServices
{
    public static class DbConfiguration
    {
        
        public static IServiceCollection  ConfigureDatabase(this IServiceCollection services)
        {
            services.AddDbContext<EmpDbContext>(option => option.UseInMemoryDatabase("EmployeeDb"));

            return services;    
        }

    }
}
