using EmployeeManagement.Contracts;
using EmployeeManagement.DB;
using EmployeeManagement.Entities;
using EmployeeManagement.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly EmpDbContext _context;

        public EmployeeServices(EmpDbContext empDbContext)
        {
            _context= empDbContext; 
            
        }
        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            try
            {

                await _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();     
                return employee;
            }
            catch (Exception ex) 
            {
                throw new AddEmployeException("unable to add employee",ex); 
            }
    
        }

        public async Task<bool> DeleteEmployeeAsync(string id)
        {
            try
            {
                var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { 
                throw new DeleteEmployeException($"{ex.Message}", ex);      
            }
        }

        public async Task<Employee> GetEmployeeByIdAsync(string id)
        {
            try
            {
                return await _context.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
            }
            catch (Exception ex) {
                throw new EmployeNotFoundException($"{ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<Employee>> GetEmployeeListAsync()
        {
            try
            {
                return await _context.Employees.AsNoTracking().ToListAsync();
            }
            catch (Exception ex) {
                throw new EmployeNotFoundException($"{ex.Message}", ex);
            }
        }

        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            try
            {
                _context.Entry(employee).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            { 
                throw new UpdateEmployeException(ex.Message, ex);        
            }
        }
        public async Task<bool> IsEmployeeExist(string emailid)
        {
            try
            {
                var emp = await _context.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Email == emailid);
                if (emp is not null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex) {
                throw new EmployeNotFoundException(ex.Message, ex);
            }
        }

        public async Task<Employee> GetEmployeeByEmailIdAsync(string email)
        {
            try
            {
                return await _context.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Email == email);
            }
            catch (Exception ex) {
                throw new EmployeNotFoundException(ex.Message, ex);
            }

        }
    }
}
