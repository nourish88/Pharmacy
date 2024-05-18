namespace Pharmacy.Application.Features.Medicines.Commands.Create;

public record CreatedMedicineResponse(int Id,string? Name, decimal? Price,int GroupId,string Message="İşlem Başarılı");

