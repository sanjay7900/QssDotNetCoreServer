using EmployeeManagement.Entities;
using FluentValidation;

namespace EmployeeManagement.FluentsValidation
{
    public class EmployeeValidation : AbstractValidator<Employee>
    {
        public EmployeeValidation()
        {
            
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 100).WithMessage("Name should be between 2 and 100 characters.");

            
            RuleFor(x => x.Salary)
                .GreaterThan(0).WithMessage("Salary must be a positive number.");

           
            RuleFor(x => x.Position)
                .NotEmpty().WithMessage("Position is required.");

            
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("A valid email address is required.");

           
            RuleFor(x => x.Department)
                .InclusiveBetween(1, 20).WithMessage("Department must be between 1 and 20.");

        }
    }
}
