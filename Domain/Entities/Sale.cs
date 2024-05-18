using Pharmacy.Shared.Bases;

namespace Pharmacy.Domain.Entities;

public class Sale : EntityBase
{
    public Sale(DateTime time)
    {
        Time = time;
        Day = time.Day;
        Month = time.Month;
        Year = time.Year;
    }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public DateTime Time { get; set; }
    public int Year { get; set; }
    public int Day { get; set; }
    public int Month { get; set; }
    public ICollection<SaleGroup>? SaleGroups { get; set; }
    public ICollection<SaleItem>? SaleItems { get; set; }
}