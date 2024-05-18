using Pharmacy.Shared.Bases;


namespace Pharmacy.Domain.Entities;

public class Group:EntityBase
{        
    public  required string Name { get; set; }
    public virtual List<Medicine>? Medicines { get; set; }
}