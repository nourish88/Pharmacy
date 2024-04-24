using AutoMapper;
using MediatR;
using Pharmacy.Application.Features.Customers.Commands.Create;
using Pharmacy.Domain.Entities;
using Pharmacy.Infrastructure;

namespace Pharmacy.Application.Features.Customers.Commands.Update;

public class UpdateCustomerCommand:IRequest<UpdatedCustomerResponse?>
{
    public string Name { get; set; }
    public int Id { get; set; }
    public string? PhoneNumber{ get; set; }
    public string? Email{ get; set; }
    public string? Address{ get; set; }
}

public class UpdateCustomerCommandHandler(IMapper mapper, AppDbContext ctx)
    : IRequestHandler<UpdateCustomerCommand, UpdatedCustomerResponse?>
{


    public async Task<UpdatedCustomerResponse?> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await ctx.Customers.FindAsync(request.Id);
        if (customer is null) return null;
        var updatedCustomer = mapper.Map<Customer>(request);
        ctx.Customers.Update(updatedCustomer);
        await ctx.SaveChangesAsync(cancellationToken);
        var res = mapper.Map<UpdatedCustomerResponse>(request);
        return res;
    }
}