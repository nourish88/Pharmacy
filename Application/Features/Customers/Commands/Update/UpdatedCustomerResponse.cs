namespace Pharmacy.Application.Features.Customers.Commands.Update;

public sealed record UpdatedCustomerResponse(string Name, int Id, string? PhoneNumber, string? Email, string? Address);