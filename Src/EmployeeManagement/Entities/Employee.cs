using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Entities
{
    public class Employee
    {
        public string? Id { get; set; } = null;  
        public string Name { get; set; }
        public float Salary {  get; set; } 
        public string Position {  get; set; }
        [EmailAddress]
        public string Email   { get; set; } 
        public int Department { get; set; }      
    }
}
