using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Features.Sales.Queries.DTOs;
using Pharmacy.Application.Features.Sales.Queries.GetById;
using Pharmacy.Infrastructure;

namespace Pharmacy.Application.Features.Sales.Queries.GetAll;

public class GetAllSaleResultsQuery : IRequest<List<SaleDto>>;

public class GetAllSaleResultsQueryHandler(IMapper mapper, AppDbContext dbContext)
    : IRequestHandler<GetAllSaleResultsQuery, List<SaleDto>>
{
    public async Task<List<SaleDto>> Handle(GetAllSaleResultsQuery request, CancellationToken cancellationToken)
    {
        var sales = dbContext.Sales;
        var result = new List<SaleDto>();
        var customers = await sales.Include(x => x.SaleItems).ThenInclude(x => x.Medicine).Include(x => x.Customer)
            .ToListAsync(cancellationToken: cancellationToken);
        foreach (var customer in customers)
        {
            var saleItemResponse = customer?.SaleItems.Select(q => new SaleItemResponse
            {
                MedicineName = q.Medicine?.Name ?? string.Empty,
                Amount = q.Amount,
                MedicineId = q.MedicineId,
                MedicinePrice = q.Medicine?.Price * q.Amount
            }).ToList();


            var response = new SaleDto
            {
                CustomerName = customer?.Customer?.Name ?? "",
                CustomerId = customer!.Customer!.Id,
                TotalSalePrice = saleItemResponse?.Sum(x => x.TotalMedicinePrice),
                SaleItemResponses = saleItemResponse
            };
            result.Add(response);
        }


        return result;
    }
}