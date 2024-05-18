using AutoMapper;
using MediatR;
using Pharmacy.Domain.Entities;
using Pharmacy.Infrastructure;

namespace Pharmacy.Application.Features.Customers.Commands.Create;

public class CreateCustomerCommand:IRequest<CreatedCustomerResponse>
{
    public string Name { get; set; }
    public string SurName { get; set; }
    public string? PhoneNumber{ get; set; }
    public string? Email{ get; set; }
    public string? Address{ get; set; }
}
public class CreateCustomerCommandHandler(AppDbContext ctx, IMapper mapper)
    : IRequestHandler<CreateCustomerCommand, CreatedCustomerResponse>
{


    public async Task<CreatedCustomerResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {   var  customer = mapper.Map<Customer>(request);
        await ctx.Customers.AddAsync(customer, cancellationToken);
        await ctx.SaveChangesAsync(cancellationToken);
        CreatedCustomerResponse createdCustomerResponse = mapper.Map<CreatedCustomerResponse>(customer);
        return createdCustomerResponse;
    }
}
