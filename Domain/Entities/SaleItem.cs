using Pharmacy.Shared.Bases;

namespace Pharmacy.Domain.Entities;

public class SaleItem : EntityBase
{
    public int SaleId { get; set; }
    public Sale? Sale { get; set; }

    public int MedicineId { get; set; }
    public Medicine? Medicine { get; set; }

    public int Amount { get; set; }
}