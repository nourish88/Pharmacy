using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Features.Customers.Queries.GetById;
using Pharmacy.Application.Features.Sales.Queries.DTOs;
using Pharmacy.Infrastructure;

namespace Pharmacy.Application.Features.Sales.Queries.GetById;

public class GetSaleResultByIdQuery: IRequest<SaleDto>
{
    public int Id { get; set; }
}

public class GetSaleResultByIdQueryHandler(IMapper mapper, AppDbContext dbContext) : IRequestHandler<GetSaleResultByIdQuery, SaleDto>
{
    public async Task<SaleDto> Handle(GetSaleResultByIdQuery request, CancellationToken cancellationToken)
    {
        var sales = dbContext.Sales;
        var  customer = await sales.Include(x=>x.SaleItems).ThenInclude(x=>x.Medicine).Include(x=>x.Customer).FirstOrDefaultAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);

        var saleItemResponse = customer?.SaleItems.Select(q => new SaleItemResponse
        {
            MedicineName = q.Medicine?.Name??string.Empty,
            Amount = q.Amount,
            MedicineId = q.MedicineId,
            MedicinePrice = q.Medicine?.Price * q.Amount
        }).ToList();

        
   
    
        var response = new SaleDto
        {
            CustomerName = customer?.Customer?.Name??"",
            CustomerId = customer!.Customer!.Id,
            TotalSalePrice = saleItemResponse?.Sum(x=>x.TotalMedicinePrice),
            SaleItemResponses = saleItemResponse
        };
     
        return response;
    }
}