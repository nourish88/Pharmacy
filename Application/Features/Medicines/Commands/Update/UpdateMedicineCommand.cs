using AutoMapper;
using MediatR;
using Pharmacy.Application.Features.Customers.Commands.Update;
using Pharmacy.Domain.Entities;
using Pharmacy.Infrastructure;

namespace Pharmacy.Application.Features.Medicines.Commands.Update;

public class UpdateMedicineCommand:IRequest<UpdatedMedicineResponse?>
{
    public string Name { get; set; }
    public int Id { get; set; }
    public decimal? Price{ get; set; }

}

public class UpdateMedicineCommandHandler(IMapper mapper, AppDbContext ctx)
    : IRequestHandler<UpdateMedicineCommand, UpdatedMedicineResponse?>
{


    public async Task<UpdatedMedicineResponse?> Handle(UpdateMedicineCommand request, CancellationToken cancellationToken)
    {
        var medicine = await ctx.Medicines.FindAsync(request.Id);
        if (medicine is null) return null;
        medicine.Name = request.Name;
        medicine.Price = request.Price;
        ctx.Medicines.Update(medicine);
        await ctx.SaveChangesAsync(cancellationToken);
        var res = mapper.Map<UpdatedMedicineResponse>(request);
        return res;
    }
}