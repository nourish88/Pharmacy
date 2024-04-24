using AutoMapper;
using MediatR;
using Pharmacy.Application.Features.Customers.Commands.Create;
using Pharmacy.Domain.Entities;
using Pharmacy.Infrastructure;

namespace Pharmacy.Application.Features.Medicines.Commands.Create;

public class CreateMedicineCommand:IRequest<CreatedMedicineResponse>
{
    public string Name { get; set; }
    public decimal? Price{ get; set; }

}
public class CreateCustomerCommandHandler(AppDbContext ctx, IMapper mapper)
    : IRequestHandler<CreateMedicineCommand, CreatedMedicineResponse>
{


    public async Task<CreatedMedicineResponse> Handle(CreateMedicineCommand request, CancellationToken cancellationToken)
    {   
        var  medicine = mapper.Map<Medicine>(request);
        await ctx.Medicines.AddAsync(medicine, cancellationToken);
        await ctx.SaveChangesAsync(cancellationToken);
        CreatedMedicineResponse createdCustomerResponse = mapper.Map<CreatedMedicineResponse>(medicine);
        return createdCustomerResponse;
    }
}
