using MediatR;
using Pharmacy.Domain.Entities;
using Pharmacy.Infrastructure;

namespace Pharmacy.Application.Features.Sales.Commands.Create;

public sealed record CreatedSaleRespond(int? Id, string? Message = "İşlem Başarılı");
public class CreateSaleCommand : IRequest<CreatedSaleRespond>
{
    public int CustomerId { get; set; }
    public string Date { get; set; }
    public List<SaleItemCreateDto> SaleItemCreateDtos { get; set; }
}

public class CreateSaleCommandHandler(AppDbContext ctx)
    : IRequestHandler<CreateSaleCommand, CreatedSaleRespond>
{


    public async Task<CreatedSaleRespond> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var sales = ctx.Sales;
    
        var saleItems = request.SaleItemCreateDtos.Select(q => new SaleItem
        {
            Amount = q.Amount,
            MedicineId = q.Id

        }).ToList();

        var date = Convert.ToDateTime(request.Date);
        var sale = new Sale(date)
        {
            CustomerId = request.CustomerId,
            SaleItems = saleItems
        };

        await sales.AddAsync(sale, cancellationToken);
        await ctx.SaveChangesAsync(cancellationToken);
         return new CreatedSaleRespond(sale.Id);


    }
}