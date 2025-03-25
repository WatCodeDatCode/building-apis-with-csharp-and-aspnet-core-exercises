using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace TheEmployeeAPI.Employees;

public class CreateEmployeeRequest
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? SocialSecurityNumber { get; set; }

    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
}

public class CreateEmployeeRequestValidator : AbstractValidator<CreateEmployeeRequest>
{
    // Commented out code = Example using potential db call and chaining validation states
    // private readonly EmployeeRepository _repository;
    public CreateEmployeeRequestValidator()
    {
        // this._repository = repository;

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First name is required.");
        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last name is required.");
        // RuleFor(x => x.SocialSecurityNumber)
        //     .Cascade(CascadeMode.Stop) // Default validates everything, this allows it to stop if any part is invalid
        //     .NotEmpty()
        //     .WithMessage("SSN cannot be empty.")
        //     .MustAsync(BeUnique)
        //     .WithMessage("SSN must be unique.");

        // Conditional validation, validate address only if address1 is not empty
        // When(r => r.Address1 != null, () => {
        //     RuleFor(x=> x.Address1).NotEmpty();
        //     RuleFor(x => x.City).NotEmpty();
        //     RuleFor(x => x.State).NotEmpty();
        //     RuleFor(x => x.ZipCode).NotEmpty();
        // });
    }

    // private async Task<bool> BeUnique(string ssn, CancellationToken token)
    // {
    //     return await _repository.GetEmployeeBySsn(ssn) != null;
    // }
}