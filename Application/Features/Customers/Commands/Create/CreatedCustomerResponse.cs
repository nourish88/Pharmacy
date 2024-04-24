namespace Pharmacy.Application.Features.Customers.Commands.Create;

public record CreatedCustomerResponse(int Id,string? Name,string? PhoneNumber,string? Email,string? Address );

