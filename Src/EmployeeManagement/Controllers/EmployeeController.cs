using EmployeeManagement.Contracts;
using EmployeeManagement.DB;
using EmployeeManagement.Entities;
using EmployeeManagement.FluentsValidation;
using EmployeeManagement.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeServices _employeeServices;

        public EmployeeController(IEmployeeServices employeeServices)
        {
            _employeeServices = employeeServices;

        }
        [HttpGet]
        [Route("GetAllEmployee")]
        public async Task<ApiResponse<IEnumerable<EmployeeResponse>>> GetAllEmployee()
        {
            var data = await _employeeServices.GetEmployeeListAsync();
            var res = data.Select(s => new EmployeeResponse()
            {
                Id = s.Id,  
                Salary = s.Salary,
                Department = s.Department,
                Email = s.Email,
                Name = s.Name,
                Position = s.Position,
                DepartmentName = ((DepartmentEnum)s.Department).ToString().Replace("_", " ")
            });
            string msg = "Data get successfully";
            if (data == null || data.Count()<1)
            {
                msg = "No Record found";
            }

            return new ApiResponse<IEnumerable<EmployeeResponse>>(res,msg);
        }

        [HttpGet]
        [Route("GetEmployeeById")]
        public async Task<ApiResponse<EmployeeResponse>> GetEmployeeById(string guid)
        {
            var data = await _employeeServices.GetEmployeeByIdAsync(guid);
            if (data is null)
            {
                return new ApiResponse<EmployeeResponse>(null, $"No Record Found {guid}",400);

            }
            return new ApiResponse<EmployeeResponse>(new EmployeeResponse()
            {
                Id=data.Id,    
                Salary = data.Salary,
                Department = data.Department,
                Email = data.Email,
                Name = data.Name,
                Position = data.Position,
                DepartmentName = ((DepartmentEnum)data.Department).ToString().Replace("-", " ")
            }, "Employee Get Successfully");
        }
        [HttpDelete]
        [Route("DeleteEmployee")]
        public async Task<ApiResponse<bool>> DeleteEmployee(string guid)
        {
            var isExists = await _employeeServices.GetEmployeeByIdAsync(guid);
            if (isExists == null)
            {
                return new ApiResponse<bool>(false, $"No Record Found {guid}", 400);

            }
            return new ApiResponse<bool>(await _employeeServices.DeleteEmployeeAsync(guid), "Employee Record Deleted Successfully");
        }
        [HttpPut]
        [Route("UpdateEmployee")]
        public async Task<ApiResponse<bool>> UpdateEmployee(Employee request)
        {
            var isExists = await _employeeServices.GetEmployeeByIdAsync(request.Id);
            if (isExists==null)
            {
                return new ApiResponse<bool>(false, $"No Record Found {request.Id}", 400);

            }
            return new ApiResponse<bool>(await _employeeServices.UpdateEmployeeAsync(request),"Employe updated successfully");
        }
        [HttpPost]
        [Route("AddEmployee")]
        public async Task<ApiResponse<Employee>> AddEmployee( Employee request)
        {
            var validator = new EmployeeValidation();            
            var result = validator.Validate(request);
            StringBuilder errorMsg= new StringBuilder();   
            if (!result.IsValid)
            {
                foreach (var failure in result.Errors)
                {
                    errorMsg.Append($"Property: {failure.PropertyName}, Error: {failure.ErrorMessage}");
                }
                return new ApiResponse<Employee>(null, $"{errorMsg}", 400);
            }
           


            DepartmentEnum departmentEnum= (DepartmentEnum)request.Department;      
            if(departmentEnum.ToString() == request.Department.ToString())
            {
                return new ApiResponse<Employee>(null, $"No department found with id {request.Department}",400);
            }
            var IsExists = await _employeeServices.IsEmployeeExist(request.Email);
            if (IsExists)
            {
                return new ApiResponse<Employee>(null, $"Email id already Exists {request.Email}", 400);

            }


            request.Id = Guid.NewGuid().ToString();
            return new ApiResponse<Employee>(await _employeeServices.AddEmployeeAsync(request),"Employee added successfully");

        }

        [HttpGet]
        [Route("GetAllDepartment")]
        public async Task<ApiResponse<List<Department>>> GetAllDepartment()
        {
            List<Department> departments = new List<Department>();
            foreach (DepartmentEnum department in Enum.GetValues(typeof(DepartmentEnum)))
            {
                departments.Add(new Department()
                {
                    Id = (int)department,
                    Name = department.ToString().Replace('_', ' ')
                });

            };
            return new ApiResponse<List<Department>>(departments);
                    
        }

    }


    
}
