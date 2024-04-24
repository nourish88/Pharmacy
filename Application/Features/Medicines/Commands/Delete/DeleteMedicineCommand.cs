using MediatR;
using Pharmacy.Application.Features.Customers.Commands.Delete;
using Pharmacy.Infrastructure;

namespace Pharmacy.Application.Features.Medicines.Commands.Delete;

public class DeleteMedicineCommand : IRequest<int?>
{
    public int Id { get; set; }
}

public class DeleteMedicineCommandHandler(AppDbContext ctx)
    : IRequestHandler<DeleteMedicineCommand, int?>
{
    public async Task<int?> Handle(DeleteMedicineCommand request, CancellationToken cancellationToken)
    {
        var medicine = await ctx.Medicines.FindAsync(request.Id);
        if (medicine is null) return null;
        ctx.Medicines.Remove(medicine);
        await ctx.SaveChangesAsync(cancellationToken);

        return request.Id;
    }
}