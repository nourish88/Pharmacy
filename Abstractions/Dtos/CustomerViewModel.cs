namespace Pharmacy.Abstractions.Dtos;

public sealed record CustomerViewModel(int Id, string? Name, string? PhoneNumber,string? Email ,string? Address);

public sealed record MedicineViewModel(int Id, string? Name, decimal? Price);
