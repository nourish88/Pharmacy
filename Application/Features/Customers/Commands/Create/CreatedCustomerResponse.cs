namespace Pharmacy.Application.Features.Customers.Commands.Create;

public record CreatedCustomerResponse(int Id,string? Name,string? SurName,string? PhoneNumber,string? Email,string? Address );

