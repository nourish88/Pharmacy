using MediatR;
using Pharmacy.Domain.Entities;
using Pharmacy.Infrastructure;

namespace Pharmacy.Application.Features.Sales.Commands.Create.SaleGroup;

public sealed record CreatedSaleGroupRespond(int? Id, string? Message = "İşlem Başarılı");
public class CreateSaleGroupCommand : IRequest<CreatedSaleGroupRespond>
{
    public int CustomerId { get; set; }
    public string Date { get; set; }
    public List<SaleGroupCreateDto> SaleGroupCreateDtos { get; set; }
}

public class CreateSaleGroupCommandHandler(AppDbContext ctx)
    : IRequestHandler<CreateSaleGroupCommand, CreatedSaleGroupRespond>
{


    public async Task<CreatedSaleGroupRespond> Handle(CreateSaleGroupCommand request, CancellationToken cancellationToken)
    {
        var sales = ctx.Sales;
    
        var saleGroups = request.SaleGroupCreateDtos.Select(q => new Domain.Entities.SaleGroup
        {
            Amount = q.Amount,
            GroupId = q.Id

        }).ToList();

        var date = Convert.ToDateTime(request.Date);
        var sale = new Sale(date)
        {
            CustomerId = request.CustomerId,
            SaleGroups = saleGroups
        };

        await sales.AddAsync(sale, cancellationToken);
        await ctx.SaveChangesAsync(cancellationToken);
         return new CreatedSaleGroupRespond(sale.Id);


    }
}