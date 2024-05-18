using Pharmacy.Shared.Bases;

namespace Pharmacy.Domain.Entities;

public class SaleGroup : EntityBase
{
    public int SaleId { get; set; }
    public Sale? Sale { get; set; }

    public int GroupId { get; set; }
    public Group? Group { get; set; }

    public int Amount { get; set; }
}