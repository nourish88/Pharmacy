using Pharmacy.Shared.Bases;

namespace Pharmacy.Domain.Entities;
public class Medicine:EntityBase
{
   
    public  string? Name { get; set; }
    public  decimal? Price { get; set; }
    public  int? GroupId { get; set; }
    public  virtual Group Group { get; set; }
}