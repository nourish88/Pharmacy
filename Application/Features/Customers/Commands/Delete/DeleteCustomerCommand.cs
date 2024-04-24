
using MediatR;
using Pharmacy.Infrastructure;
namespace Pharmacy.Application.Features.Customers.Commands.Delete;

public class DeleteCustomerCommand : IRequest<int?>
{
    public int Id { get; set; }
}

public class DeleteCustomerCommandHandler(AppDbContext ctx)
    : IRequestHandler<DeleteCustomerCommand, int?>
{
    public async Task<int?> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await ctx.Customers.FindAsync(request.Id);
        if (customer is null) return null;
        ctx.Customers.Remove(customer);
        await ctx.SaveChangesAsync(cancellationToken);

        return request.Id;
    }
}