namespace Pharmacy.Application.Features.Medicines.Queries.GetById;

public sealed record GetByIdMedicineResponse(int Id , string? Name, decimal? Price);

