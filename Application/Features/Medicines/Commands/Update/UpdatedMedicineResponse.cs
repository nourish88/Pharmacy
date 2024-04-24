namespace Pharmacy.Application.Features.Customers.Commands.Update;

public sealed record UpdatedMedicineResponse(string Name, int Id, decimal? Price);