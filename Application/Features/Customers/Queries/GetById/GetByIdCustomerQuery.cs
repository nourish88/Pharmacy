using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Infrastructure;

namespace Pharmacy.Application.Features.Customers.Queries.GetById;

public class GetByIdCustomerQuery: IRequest<GetByIdCustomerResponse>
{
    public  int Id { get; set; }
}

public class GetByIdCustomerQueryHandler(IMapper mapper, AppDbContext dbContext) : IRequestHandler<GetByIdCustomerQuery, GetByIdCustomerResponse>
{
    public async Task<GetByIdCustomerResponse> Handle(GetByIdCustomerQuery request, CancellationToken cancellationToken)
    {
        var customerRepo = dbContext.Customers;
        var  customer = await customerRepo.FirstOrDefaultAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);

        var response = mapper.Map<GetByIdCustomerResponse>(customer);
        return response;
    }
}