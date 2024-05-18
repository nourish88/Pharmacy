using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Features.Sales.Queries.DTOs;
using Pharmacy.Infrastructure;

namespace Pharmacy.Application.Features.Sales.Queries.Get
{
    public class GroupSaleQuery : IRequest<GroupSaleQueryResponse>
    {
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public int? GroupId { get; set; }
        public int? CustomerId { get; set; }

    }
    public class GroupSaleQueryHandler(IMapper mapper, AppDbContext dbContext) : IRequestHandler<GroupSaleQuery, GroupSaleQueryResponse>
    {
        //public async Task<GetAllMedicinesResponse> Handle(GetAllMedicinesQuery request, CancellationToken cancellationToken)
        //{
        //    var customerRepo = dbContext.Medicines;
        //    var customer = await customerRepo.ToListAsync(cancellationToken: cancellationToken);

        //    var response = mapper.Map<List<BaseMedicineViewModel>>(customer);
        //    var model = new GetAllMedicinesResponse { Medicines = response };
        //    return model;
        //}
        public async Task<GroupSaleQueryResponse> Handle(GroupSaleQuery request, CancellationToken cancellationToken)
        {
            var salesRepo = dbContext.Sales;
           
            var queryable = salesRepo.Include(x => x.SaleGroups).AsQueryable();
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
                .SelectMany(s => s.SaleGroups, (sale, saleItem) => new { sale.Customer, GroupName = saleItem.Group!.Name, saleItem.Amount, GroupId = saleItem.GroupId })
                .GroupBy(x => new { x.GroupName, x.GroupId, x.Customer.Name, x.Customer.SurName })
                .Select(g => new GroupSaleDto
                {
                    GroupName = g.Key.GroupName,
                    Amount = g.Sum(x => x.Amount),
                    CustomerName = $"{g.Key.Name} {g.Key.SurName}",
                    GroupId = g.Key.GroupId

                });
            if (request.GroupId != null)
            {
                grouped = grouped.Where(x => x.GroupId == request.GroupId);
            }
            var model = await grouped.ToListAsync(cancellationToken: cancellationToken);

            var result = new GroupSaleQueryResponse
            {
                List = model,
                TotalCount = 0
            };

            return result;
        }
    }
}
