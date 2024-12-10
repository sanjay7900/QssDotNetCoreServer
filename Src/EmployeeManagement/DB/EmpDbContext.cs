using EmployeeManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.DB
{
    public class EmpDbContext:DbContext
    {
        public DbSet<Employee> Employees { get; set; }      
        public EmpDbContext(DbContextOptions<EmpDbContext> dbContextOptions):base(dbContextOptions) { }     
        
    }
}
