using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Abstractions.Dtos;
using Pharmacy.Application.Features.Customers.Queries.GetByName;
using Pharmacy.Infrastructure;

namespace Pharmacy.Application.Features.Medicines.Queries.GetByName;

public class GetByNameMedicineQuery : IRequest<GetByNameMedicinesResponse>
{
    public  string? Name { get; set; }
    public int? Id { get; set; }
    public int Skip { get; set; } = 0;

    public int Take { get; set; } = 10;
    public string? GroupName { get; set; } 

}

public class GetByNameMedicineQueryHandler(IMapper mapper, AppDbContext dbContext) : IRequestHandler<GetByNameMedicineQuery, GetByNameMedicinesResponse>
{
    public async Task<GetByNameMedicinesResponse> Handle(GetByNameMedicineQuery request, CancellationToken cancellationToken)
    {
        var medicineRepo = dbContext.Medicines.Include(x=>x.Group).AsQueryable();
        if (!string.IsNullOrWhiteSpace(request.Name)&& request.Name!="null")
        {
            medicineRepo = medicineRepo.Where(predicate: b => b.Name!.Contains(request.Name));
        }
        if (!string.IsNullOrWhiteSpace(request.GroupName) && request.GroupName != "null")
        {
            medicineRepo = medicineRepo.Where(predicate: b => b.Group.Name!.Contains(request.GroupName));
        }
        var  medicines = await medicineRepo.Skip(request.Skip* request.Take).Take(request.Take).ToListAsync(cancellationToken: cancellationToken);

        var response = mapper.Map<List<MedicineViewModel>>(medicines);
        var model = new GetByNameMedicinesResponse { Medicines = response, TotalRecords = await medicineRepo.CountAsync()};
        return model;
    }
}