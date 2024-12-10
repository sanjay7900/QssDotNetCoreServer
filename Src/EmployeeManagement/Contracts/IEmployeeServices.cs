using EmployeeManagement.Entities;

namespace EmployeeManagement.Contracts
{
    public interface IEmployeeServices
    {
       Task<IEnumerable<Employee>> GetEmployeeListAsync();
       Task<Employee> GetEmployeeByIdAsync(string id);
       Task<Employee> GetEmployeeByEmailIdAsync(string email);
       Task<Employee> AddEmployeeAsync(Employee employee);
       Task<bool> DeleteEmployeeAsync(string id);
       Task<bool> UpdateEmployeeAsync(Employee employee); 
       Task<bool> IsEmployeeExist(string emailid);
        

       
    }
}
