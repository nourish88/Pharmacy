namespace Pharmacy.Application.Features.Sales.Queries.DTOs;


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
   

   