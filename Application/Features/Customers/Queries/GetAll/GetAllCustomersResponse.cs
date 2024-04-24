using Pharmacy.Abstractions.Dtos;

namespace Pharmacy.Application.Features.Customers.Queries.GetAll;

public class GetAllCustomersResponse
{
    public List<CustomerViewModel> Customers { get; set; }
}