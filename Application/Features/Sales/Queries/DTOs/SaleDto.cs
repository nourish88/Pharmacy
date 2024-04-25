namespace Pharmacy.Application.Features.Sales.Queries.DTOs;


    public sealed record SaleDto
    {
        public string CustomerName { get; set; }
        public int CustomerId { get; set; }
        public decimal? TotalSalePrice { get; set; }

        public List<SaleItemResponse>? SaleItemResponses { get; set; } 
    };
    public sealed record SaleItemResponse
    {
        public string MedicineName { get; set; }
        public int MedicineId { get; set; }
        public decimal? MedicinePrice { get; set; }
        public decimal? TotalMedicinePrice => MedicinePrice * Amount;
        public int Amount { get; set; }
    };

   