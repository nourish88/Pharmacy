using MediatR;
using Pharmacy.Domain.Entities;
using Pharmacy.Infrastructure;

namespace Pharmacy.Application.Features.Sales.Commands.Create;

public class CreateSaleCommand:IRequest<int>
{
    public int CustomerId { get; set; }
    public DateOnly Date { get; set; }
    public List<SaleItemCreateDto> SaleItemCreateDtos { get; set; }
}

public class CreateSaleCommandHandler(AppDbContext ctx)
    : IRequestHandler<CreateSaleCommand, int >
{


    public async Task<int> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var sales = ctx.Sales;
        var saleItems = request.SaleItemCreateDtos.Select(q => new SaleItem
        {
            Amount = q.Amount,
            MedicineId= q.MedicineId
            
        }).ToList();
        var sale = new Sale
        {
            Time = request.Date,
            CustomerId = request.CustomerId,
            SaleItems = saleItems
        };
        await sales.AddAsync(sale, cancellationToken);
        await ctx.SaveChangesAsync(cancellationToken);
        return sale.Id;
    }
}