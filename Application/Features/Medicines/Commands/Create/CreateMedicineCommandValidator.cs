using FluentValidation;
using Pharmacy.Application.Features.Customers.Commands.Create;

namespace Pharmacy.Application.Features.Medicines.Commands.Create;

public class CreateMedicineCommandValidator:AbstractValidator<CreateMedicineCommand>
{
    public CreateMedicineCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MinimumLength(2);
        
    }
    
}


