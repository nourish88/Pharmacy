using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Abstractions.Dtos;
using Pharmacy.Application.Features.Customers.Queries.GetByName;
using Pharmacy.Infrastructure;

namespace Pharmacy.Application.Features.Medicines.Queries.GetByName;

public class GetByNameMedicineQuery : IRequest<GetByNameMedicinesResponse>
{
    public  string Name { get; set; }
}

public class GetByNameMedicineQueryHandler(IMapper mapper, AppDbContext dbContext) : IRequestHandler<GetByNameMedicineQuery, GetByNameMedicinesResponse>
{
    public async Task<GetByNameMedicinesResponse> Handle(GetByNameMedicineQuery request, CancellationToken cancellationToken)
    {
        var medicineRepo = dbContext.Medicines;
        var  medicine = await medicineRepo.Where(predicate: b => b.Name == request.Name).ToListAsync(cancellationToken: cancellationToken);

        var response = mapper.Map<List<MedicineViewModel>>(medicine);
        var model = new GetByNameMedicinesResponse { Medicines = response };
        return model;
    }
}