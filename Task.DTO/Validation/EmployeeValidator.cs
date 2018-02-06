using FluentValidation;
using Task.DTO.Request;

namespace Task.DTO.Validation
{
    public class EmployeeValidator : AbstractValidator<EmployeeRequest>
    {
        public EmployeeValidator()
        {
            RuleFor(e => e.FirstName)
                .NotEmpty()
                .WithMessage("The First Name cannot be blank.")
                .MaximumLength(50)
                .WithMessage("The First Name cannot be more than 50 characters.")
                .Matches(@"^[a-zA-Z ]+$")
                .WithMessage("The First Name must not contain numbers and special characters.");

            RuleFor(e => e.LastName)
                .NotEmpty()
                .WithMessage("The Last Name cannot be blank.")
                .MaximumLength(50)
                .WithMessage("The Last Name cannot be more than 50 characters.")
                .Matches(@"^[a-zA-Z ]+$")
                .WithMessage("The Last Name must not contain numbers and special characters.");
        }
    }
}
