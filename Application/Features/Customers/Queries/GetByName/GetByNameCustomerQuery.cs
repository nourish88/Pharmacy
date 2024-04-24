using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Abstractions.Dtos;
using Pharmacy.Infrastructure;

namespace Pharmacy.Application.Features.Customers.Queries.GetByName;

public class GetByNameCustomerQuery : IRequest<GetByNameCustomersResponse>
{
    public  string Name { get; set; }
}

public class GetByNameCustomerQueryHandler(IMapper mapper, AppDbContext dbContext) : IRequestHandler<GetByNameCustomerQuery, GetByNameCustomersResponse>
{
    public async Task<GetByNameCustomersResponse> Handle(GetByNameCustomerQuery request, CancellationToken cancellationToken)
    {
        var customerRepo = dbContext.Customers;
        var  customer = await customerRepo.Where(predicate: b => b.Name == request.Name).ToListAsync(cancellationToken: cancellationToken);

        var response = mapper.Map<List<CustomerViewModel>>(customer);
        var model = new GetByNameCustomersResponse { Customers = response };
        return model;
    }
}