using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Abstractions.Dtos;
using Pharmacy.Application.Features.Customers.Queries.GetAll;
using Pharmacy.Infrastructure;

namespace Pharmacy.Application.Features.Medicines.Queries.GetAll;

public class GetAllMedicinesQuery : IRequest<GetAllMedicinesResponse>;

public class GetAllMedicinessQueryHandler(IMapper mapper, AppDbContext dbContext) : IRequestHandler<GetAllMedicinesQuery, GetAllMedicinesResponse>
{
    public async Task<GetAllMedicinesResponse> Handle(GetAllMedicinesQuery request, CancellationToken cancellationToken)
    {
        var customerRepo = dbContext.Medicines;
        var  customer = await customerRepo.ToListAsync(cancellationToken: cancellationToken);

        var response = mapper.Map<List<BaseMedicineViewModel>>(customer);
        var model = new GetAllMedicinesResponse { Medicines = response };
        return model;
    }
}