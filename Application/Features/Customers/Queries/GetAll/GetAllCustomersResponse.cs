using Pharmacy.Abstractions.Dtos;

namespace Pharmacy.Application.Features.Customers.Queries.GetAll;

public class GetAllCustomersResponse
{
    public int CurrentPage { get; set; }
    public int TotalRecords { get; set; }
    public List<CustomerViewModel> Customers { get; set; }
}