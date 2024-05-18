namespace Pharmacy.Application.Features.Sales.Commands.Create.SaleGroup;

public record CreatedSaleGroupResponse
{
    public string CustomerName { get; set; }
    public int CustomerId { get; set; }
    public decimal TotalSalePrice => SaleGroupResponses.Sum(x => x.TotalMedicinePrice);

    public List<SaleGroupResponse> SaleGroupResponses { get; set; } = new();
};
public sealed record SaleGroupResponse
{
    public string Grouop { get; set; }
    public int GroupId { get; set; }
    public decimal MedicinePrice { get; set; }
    public decimal TotalMedicinePrice => MedicinePrice * Amount;
    public int Amount { get; set; }
};

public sealed record SaleGroupCreateDto(int Id, int Amount);
