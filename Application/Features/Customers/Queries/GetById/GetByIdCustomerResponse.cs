namespace Pharmacy.Application.Features.Customers.Queries.GetById;

public sealed record GetByIdCustomerResponse(int Id , string? Name, string? SurName ,string? PhoneNumber,string? Email ,string? Address);

