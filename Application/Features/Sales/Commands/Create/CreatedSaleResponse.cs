namespace Pharmacy.Application.Features.Sales.Commands.Create;

public record CreatedSaleResponse
{
    public string CustomerName { get; set; }
    public int CustomerId { get; set; }
    public decimal TotalSalePrice => SaleItemResponses.Sum(x => x.TotalMedicinePrice);

    public List<SaleItemResponse> SaleItemResponses { get; set; } = new();
};
public sealed record SaleItemResponse
{
    public string MedicineName { get; set; }
    public int MedicineId { get; set; }
    public decimal MedicinePrice { get; set; }
    public decimal TotalMedicinePrice => MedicinePrice * Amount;
    public int Amount { get; set; }
};

public sealed record SaleItemCreateDto
{
    
    public int MedicineId { get; set; }
    public decimal MedicinePrice { get; set; }
    public int Amount { get; set; }
};