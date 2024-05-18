using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Features.Customers.Commands.Create;
using Pharmacy.Domain.Entities;
using Pharmacy.Infrastructure;

namespace Pharmacy.Application.Features.Medicines.Commands.Create;

public class CreateMedicineCommand:IRequest<CreatedMedicineResponse>
{
    public string Name { get; set; }
    public decimal? Price{ get; set; }
    public int GroupId{ get; set; }

}
public class CreateCustomerCommandHandler(AppDbContext ctx, IMapper mapper)
    : IRequestHandler<CreateMedicineCommand, CreatedMedicineResponse>
{


    public async Task<CreatedMedicineResponse> Handle(CreateMedicineCommand request, CancellationToken cancellationToken)
    {
        
        
            var exist = await ctx.Medicines.AnyAsync(x =>x.Name==request.Name && x.GroupId == request.GroupId,
                cancellationToken: cancellationToken);
        
      
        if (exist)
        {
            return new CreatedMedicineResponse(0, string.Empty, 0,0, "Aynı kayıt zaten eklenmiş");
        }
            var medicine = mapper.Map<Medicine>(request);
           
            await ctx.Medicines.AddAsync(medicine, cancellationToken);
            await ctx.SaveChangesAsync(cancellationToken);
            CreatedMedicineResponse createdCustomerResponse = mapper.Map<CreatedMedicineResponse>(medicine);
            return createdCustomerResponse;
       
     
    }
}
