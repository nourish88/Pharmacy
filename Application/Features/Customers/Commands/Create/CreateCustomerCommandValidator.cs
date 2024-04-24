using FluentValidation;

namespace Pharmacy.Application.Features.Customers.Commands.Create;

public class CreateCustomerCommandValidator:AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MinimumLength(2);
        RuleFor(c => c.Email).EmailAddress();
    }
    
}


