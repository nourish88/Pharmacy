using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Abstractions.Dtos;
using Pharmacy.Infrastructure;

namespace Pharmacy.Application.Features.Customers.Queries.GetAll;

public class GetAllCustomersQuery : IRequest<GetAllCustomersResponse>;

public class GetAllCustomersQueryHandler(IMapper mapper, AppDbContext dbContext) : IRequestHandler<GetAllCustomersQuery, GetAllCustomersResponse>
{
    public async Task<GetAllCustomersResponse> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var customerRepo = dbContext.Customers;
        var customer = await customerRepo.ToListAsync(cancellationToken: cancellationToken);

        var response = mapper.Map<List<CustomerViewModel>>(customer);
        var model = new GetAllCustomersResponse { Customers = response };
        return model;
    }
}

public class GetBaseCustomersQuery : IRequest<GetAllCustomersResponse>;

public class GetBaseCustomersQueryHandler(IMapper mapper, AppDbContext dbContext) : IRequestHandler<GetBaseCustomersQuery, GetAllCustomersResponse>
{
    public async Task<GetAllCustomersResponse> Handle(GetBaseCustomersQuery request, CancellationToken cancellationToken)
    {
        var customerRepo = dbContext.Customers;
        var customers = await customerRepo.Select(q => new CustomerViewModel
        (
            q.Id,
            $"{q.Name} {q.SurName}",
            q.SurName,
            q.PhoneNumber,


             q.Email,
            q.Address




        )).ToListAsync(cancellationToken: cancellationToken);


        var model = new GetAllCustomersResponse { Customers = customers };
        return model;
    }
}