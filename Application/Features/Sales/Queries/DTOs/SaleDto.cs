namespace Pharmacy.Application.Features.Sales.Queries.DTOs;

public sealed record MedicineSaleDto
{
    
    public string CustomerName { get; set; }
    public string MedicineName { get; set; }
    public int MedicineId { get; set; }
    public int? Amount { get; set; }
  
}
public sealed record GroupSaleDto
{

    public string CustomerName { get; set; }
    public string GroupName { get; set; }
    public int GroupId { get; set; }
    public int? Amount { get; set; }

}
public sealed record GroupSaleQueryResponse
{

    public List<GroupSaleDto> List { get; set; }
    public int TotalCount { get; set; }

}
public sealed record MedicineSaleQueryResponse
{

    public List<MedicineSaleDto> List { get; set; }
    public int TotalCount { get; set; }

}
public sealed record SaleDto
    {
        public string? CustomerName { get; set; }
        public string? CustomerSurName { get; set; }
        public int CustomerId { get; set; }
        public decimal? TotalSalePrice { get; set; }

        public List<SaleItemResponse>? SaleItemResponses { get; set; } 
    };
//
    public sealed record SaleItemResponse(
        string? MedicineName,
        int MedicineId,
        decimal? MedicinePrice,
        decimal? TotalMedicinePrice,
        int Amount);
   

   