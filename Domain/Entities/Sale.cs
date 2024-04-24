using Pharmacy.Shared.Bases;

namespace Pharmacy.Domain.Entities;

public class Sale : EntityBase
{
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public DateOnly Time { get; set; }
    public int Year =>Time.Year;
    public int Day => Time.Day;
    public int Month => Time.Month;

    public ICollection<SaleItem> SaleItems { get; set; }
}