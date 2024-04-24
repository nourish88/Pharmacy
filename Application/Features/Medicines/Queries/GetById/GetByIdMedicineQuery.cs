using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Infrastructure;

namespace Pharmacy.Application.Features.Medicines.Queries.GetById;

public class GetByIdMedicineQuery: IRequest<GetByIdMedicineResponse>
{
    public  int Id { get; set; }
}

public class GetByIdMedicineQueryHandler(IMapper mapper, AppDbContext dbContext) : IRequestHandler<GetByIdMedicineQuery, GetByIdMedicineResponse>
{
    public async Task<GetByIdMedicineResponse> Handle(GetByIdMedicineQuery request, CancellationToken cancellationToken)
    {
        var customerRepo = dbContext.Medicines;
        var  customer = await customerRepo.FirstOrDefaultAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);

        var response = mapper.Map<GetByIdMedicineResponse>(customer);
        return response;
    }
}