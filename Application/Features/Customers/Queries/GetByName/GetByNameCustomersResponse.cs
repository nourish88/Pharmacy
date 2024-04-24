using Pharmacy.Abstractions.Dtos;

namespace Pharmacy.Application.Features.Customers.Queries.GetByName;

public class GetByNameCustomersResponse
{
    public List<CustomerViewModel> Customers { get; set; }

}