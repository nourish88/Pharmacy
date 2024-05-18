using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Abstractions.Dtos;
using Pharmacy.Application.Features.Medicines.Queries.GetAll;
using Pharmacy.Application.Features.Sales.Queries.DTOs;
using Pharmacy.Infrastructure;

namespace Pharmacy.Application.Features.Sales.Queries.Get;

public class MedicineSaleQuery : IRequest<MedicineSaleQueryResponse>
{
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public int? MedicineId { get; set; }
    public int? CustomerId { get; set; }
   
}
public class MedicineSaleQueryHandler(IMapper mapper, AppDbContext dbContext) : IRequestHandler<MedicineSaleQuery, MedicineSaleQueryResponse>
{
    //public async Task<GetAllMedicinesResponse> Handle(GetAllMedicinesQuery request, CancellationToken cancellationToken)
    //{
    //    var customerRepo = dbContext.Medicines;
    //    var customer = await customerRepo.ToListAsync(cancellationToken: cancellationToken);

    //    var response = mapper.Map<List<BaseMedicineViewModel>>(customer);
    //    var model = new GetAllMedicinesResponse { Medicines = response };
    //    return model;
    //}
    public async Task<MedicineSaleQueryResponse> Handle(MedicineSaleQuery request, CancellationToken cancellationToken)
    {
        var salesRepo = dbContext.Sales;
        var saleItemsRepo = dbContext.SaleItems;
        var queryable = salesRepo.Include(x => x.SaleItems).AsQueryable();
        if (!string.IsNullOrWhiteSpace(request.StartDate))
        {
            var stDate = Convert.ToDateTime(request.StartDate);
            queryable = queryable.Where(x => x.Time >= stDate);
        }
        if (!string.IsNullOrWhiteSpace(request.EndDate))
        {
            var stDate = Convert.ToDateTime(request.EndDate);
            queryable = queryable.Where(x => x.Time <= stDate);
        }

        //if (request.MedicineId != null)
        //{
        //    queryable = queryable.Where(x => x.SaleItems.Any(si => si.MedicineId == request.MedicineId));
        //}
        if (request.CustomerId != null)
        {
            queryable = queryable.Where(x => x.CustomerId == request.CustomerId);
        }
        //var count= await queryable.CountAsync(cancellationToken: cancellationToken);
        var grouped = queryable
            .SelectMany(s => s.SaleItems, (sale, saleItem) => new { sale.Customer, MedicineName = saleItem.Medicine.Name, saleItem.Amount, MedicineId = saleItem.MedicineId })
            .GroupBy(x => new { x.MedicineName, x.MedicineId, x.Customer.Name, x.Customer.SurName })
            .Select(g => new MedicineSaleDto
            {
                MedicineName = g.Key.MedicineName,
                Amount = g.Sum(x => x.Amount),
                CustomerName = $"{g.Key.Name} {g.Key.SurName}",
                MedicineId = g.Key.MedicineId

            });
        if (request.MedicineId != null)
        {
            grouped = grouped.Where(x => x.MedicineId == request.MedicineId);
        }
        var model = await grouped.ToListAsync(cancellationToken: cancellationToken);
        
        var result = new MedicineSaleQueryResponse
        {
            List = model,
            TotalCount = 0
        };

        return result;
    }
}